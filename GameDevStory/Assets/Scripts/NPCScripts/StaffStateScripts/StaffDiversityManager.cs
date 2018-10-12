/*
 * This class keeps track of all staff hired, and takes care of throwing up events as needed for lack of diversity etc.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DialogueScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace NPCScripts.StaffStateScripts
{
    public class StaffDiversityManager : Singleton<StaffDiversityManager>
    {
        private const double ProportionBeforePenaltyGender = 0.4;
        private const double ProportionBeforePenaltyAge = 0.25;
        private const double ScoreGrowRate = 0.1;

        private const double GenderDialogueThreshold = -10;
        private const double AgeDialogueThreshold = -15;

        private const double GenderNormalRecovery = -2;
        private const double GenderIgnoreRecovery = -5;

        private bool _currentlyDisplaying = false;

        private float _update = 0.0f;

        private double _diversityScore;

        public double DiversityScore
        {
            get { return _diversityScore; }
        }

        private void RecalculateMentalState(Dictionary<GameObject, NPCInfo> npcs)
        {
            var femaleProportion = npcs.Where(npc => npc.Value.Attributes.gender == NPCAttributes.Gender.FEMALE)
                .Sum(npc => (1.0 / npcs.Count));

            UpdateDiversityScore(femaleProportion, npcs.Count);

            // now update each NPC
            foreach (var npc in npcs)
            {
                // Update GenderDiversity
                if (femaleProportion < ProportionBeforePenaltyGender &&
                    npc.Value.Attributes.gender == NPCAttributes.Gender.FEMALE)
                {
                    // not enough females
                    npc.Value.MentalState.GenderDiversityScore -= (1 - femaleProportion);
                }
                else if ((1 - femaleProportion) < ProportionBeforePenaltyGender &&
                         npc.Value.Attributes.gender == NPCAttributes.Gender.MALE)
                {
                    // not enough males
                    npc.Value.MentalState.GenderDiversityScore -= femaleProportion;
                }
                else
                {
                    // slowly regrow
                    if (npc.Value.MentalState.GenderDiversityScore < 0)
                    {
                        npc.Value.MentalState.GenderDiversityScore += ScoreGrowRate;
                    }
                    else
                    {
                        npc.Value.MentalState.StaffStateGender = StaffMentalState.State.NORMAL;
                    }
                }

                // Update AgeDiversity
                var similarAge = from n in npcs.Values
                    where n.Attributes.age > (npc.Value.Attributes.age - 15) &&
                          n.Attributes.age < (npc.Value.Attributes.age + 15)
                    select n;

                var similarAgeProportion = similarAge.Count() / (double) npcs.Count;

                if (similarAgeProportion < ProportionBeforePenaltyAge)
                {
                    // not enough similar age
                    npc.Value.MentalState.AgeDiversityScore -= (1 - similarAgeProportion);
                }
                else
                {
                    // slowly regrow
                    if (npc.Value.MentalState.AgeDiversityScore < 0)
                    {
                        npc.Value.MentalState.AgeDiversityScore += ScoreGrowRate;
                    }
                    else
                    {
                        npc.Value.MentalState.StaffStateAge = StaffMentalState.State.NORMAL;
                    }
                }

                //Debug.Log("Updated NPC " + npc.Value.Attributes.npcName + " genderDiversity = " +
                //          npc.Value.MentalState.GenderDiversityScore + ", ageDiversity = " +
                //          npc.Value.MentalState.AgeDiversityScore + ", stateGender = " +
                //          npc.Value.MentalState.StaffStateGender + ", stateAge = " +
                //          npc.Value.MentalState.StaffStateAge);

                // Check if we need to take action
                if ((npc.Value.MentalState.GenderDiversityScore < GenderDialogueThreshold && !_currentlyDisplaying) ||
                    (npc.Value.MentalState.AgeDiversityScore < AgeDialogueThreshold && !_currentlyDisplaying))
                {
                    var isAgeDialogue = npc.Value.MentalState.AgeDiversityScore < AgeDialogueThreshold;
                    if ((npc.Value.MentalState.StaffStateGender == StaffMentalState.State.READY_TO_LEAVE ||
                         npc.Value.MentalState.GenderDiversityScore < GenderDialogueThreshold * 2) ||
                        (npc.Value.MentalState.StaffStateAge == StaffMentalState.State.READY_TO_LEAVE ||
                         npc.Value.MentalState.AgeDiversityScore < AgeDialogueThreshold * 2)
                    ) // special case if last dialogue ignored?                    
                    {
                        // Leave company!
                        _currentlyDisplaying = true;
                        NPCController.Instance.RemoveNPC(npc.Key);
                        ProjectManager.Instance.PauseProject();
                        DialogueManager.Instance.StartDialogue(GenerateLeaveDialogue(npc.Value));
                    }
                    else
                    {
                        Debug.Log("Throwing dialogue! (Gender)");
                        var dialogue = GenerateDialogue(npc.Value, isAgeDialogue);
                        // Pop dialogue
                        _currentlyDisplaying = true;
                        NPCController.Instance.ShowScenarioNotification(delegate
                        {
                            ProjectManager.Instance.PauseProject();
                            DialogueManager.Instance.StartDialogue(dialogue);
                        }, npc.Key);
                    }
                }
            }

            if ((femaleProportion < 0.001 || femaleProportion > 0.999) && npcs.Count > 2
            ) // getting around float accuracy errors
            {
                // randomly pick a NPC to throw the dialogue onto
                var rand = new Random();
                var npc = npcs.ElementAt(rand.Next(0, npcs.Count));
                Debug.Log("Throwing dialogue! (EveryoneIsSameGender)");
                var dialogue = GenerateSameGenderDialogue(npc.Value);
                // Pop dialogue
                _currentlyDisplaying = true;
                NPCController.Instance.ShowScenarioNotification(delegate
                {
                    ProjectManager.Instance.PauseProject();
                    DialogueManager.Instance.StartDialogue(dialogue);
                }, npc.Key);
            }
        }

        private void UpdateDiversityScore(double femaleProportion, int numberNPCs)
        {
            var deltaFromBalance = Math.Abs(femaleProportion - 0.5); // max = 0.5
            numberNPCs = (numberNPCs > 10) ? 10 : numberNPCs; // cap at 10
            var weightedScore = deltaFromBalance * numberNPCs; // max = 5
            _diversityScore = weightedScore / 5.0; // more magic numbers!!!
            // DiversityScore should be between 0 and 1.
        }

        private Dialogue GenerateLeaveDialogue(NPCInfo npc)
        {
            return new Dialogue
            {
                Sentences = new Sentence[]
                {
                    new Sentence()
                    {
                        icon = npc.Attributes.headshot,
                        Title = npc.Attributes.npcName,
                        sentenceLine = npc.Attributes.npcName + " has resigned. " + GetPronoun(npc, true) +
                                       " has moved on to a more inclusive workplace. " +
                                       "Maintaining a diverse workforce helps improve employee satisfaction and productivity. " +
                                       "You have paid a $500 penalty for lost productivity.",
                        sentenceChoices = new[] {"OK"},
                        sentenceChoiceActions = new UnityAction[]
                        {
                            delegate
                            {
                                GameManager.Instance.changeBalance(-500);
                                ProjectManager.Instance.ResumeProject();
                                _currentlyDisplaying = false;
                            },
                        }
                    }
                }
            };
        }

        private Dialogue GenerateSameGenderDialogue(NPCInfo npc)
        {
            return new Dialogue
            {
                Sentences = new Sentence[]
                {
                    new Sentence()
                    {
                        icon = npc.Attributes.headshot,
                        Title = npc.Attributes.npcName,
                        sentenceLine = npc.Attributes.npcName + " finds the office boring. Hire a bigger variety of" +
                                       " people to make the office more interesting for " +
                                       GetPronoun(npc, false).ToLower() + ".",
                        sentenceChoices = new[] {"OK"},
                        sentenceChoiceActions = new UnityAction[]
                        {
                            delegate
                            {
                                ProjectManager.Instance.ResumeProject();
                                _currentlyDisplaying = false;
                            },
                        }
                    }
                }
            };
        }

        private Dialogue GenerateDialogue(NPCInfo npc, bool isAgeDialogue)
        {
            string sentence;
            StaffMentalState.State nextState;
            string[] choices;
            var currentState = isAgeDialogue ? npc.MentalState.StaffStateAge : npc.MentalState.StaffStateGender;
            switch (currentState)
            {
                case StaffMentalState.State.NORMAL:
                    sentence = npc.Attributes.npcName + " thinks the office is boring. " + GetPronoun(npc, true) +
                               " feels out of place.\n" +
                               "Hire some like-minded coworkers to improve their mood.";
                    nextState = StaffMentalState.State.ANNOYED;
                    choices = new string[]
                    {
                        "OK",
                    };
                    break;
                case StaffMentalState.State.ANNOYED:
                    sentence = npc.Attributes.npcName + " doesn't know many people here and feels lonely.\n" +
                               "Encourage your employees to get to know " + GetPronoun(npc, false).ToLower() +
                               " by hosting a pizza party.";
                    nextState = StaffMentalState.State.ABOUT_TO_LEAVE;
                    choices = new string[]
                    {
                        "Throw Party",
                        "Ignore"
                    };
                    break;
                case StaffMentalState.State.ABOUT_TO_LEAVE:
                    var similar = isAgeDialogue ? "a similar age" : "the same gender";
                    sentence = npc.Attributes.npcName + " feels excluded by " + GetPronoun(npc, false).ToLower() +
                               " coworkers. You could help "+ GetPronoun(npc, false).ToLower() +" feel included by hiring others with " + similar + "." +
                               " Host a team-building event to improve workspace culture.";
                    nextState = StaffMentalState.State.READY_TO_LEAVE;
                    choices = new string[]
                    {
                        "Host team-building event",
                        "Ignore"
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", npc.MentalState.StaffStateGender, null);
            }

            return new Dialogue
            {
                Sentences = new Sentence[]
                {
                    new Sentence()
                    {
                        icon = npc.Attributes.headshot,
                        Title = npc.Attributes.npcName,
                        sentenceLine = sentence,
                        sentenceChoices = choices,
                        sentenceChoiceActions = new UnityAction[]
                        {
                            delegate()
                            {
                                Debug.Log("Normal option picked");
                                npc.MentalState.GenderDiversityScore = GenderNormalRecovery;
                                npc.MentalState.StaffStateGender = nextState;
                                Debug.Log("Setting state to " + npc.MentalState.StaffStateGender);
                                ProjectManager.Instance.ResumeProject();
                                _currentlyDisplaying = false;
                            },
                            delegate()
                            {
                                Debug.Log("Ignore option picked");
                                npc.MentalState.GenderDiversityScore = GenderIgnoreRecovery;
                                npc.MentalState.StaffStateGender = nextState;
                                Debug.Log("Setting state to " + npc.MentalState.StaffStateGender);
                                ProjectManager.Instance.ResumeProject();
                                _currentlyDisplaying = false;
                            },
                        },
                    }
                }
            };
        }

        private void Update()
        {
            _update += Time.deltaTime;
            // TODO: tick increased to 1s for testing!!!
            if (!(_update > 1.0f) || ProjectManager.Instance.IsPaused()) return;
            // run every 5 seconds
            _update = 0.0f;
            RecalculateMentalState(NPCController.Instance.NpcInstances);
        }

        private static string GetPronoun(NPCInfo npc, bool type)
        {
            switch (npc.Attributes.gender)
            {
                case NPCAttributes.Gender.MALE:
                    return type ? "He" : "Him";
                case NPCAttributes.Gender.FEMALE:
                    return type ? "She" : "Her";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}