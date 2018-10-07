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

namespace NPCScripts.StaffStateScripts
{
    public class StaffStateDialogueManager : Singleton<StaffStateDialogueManager>
    {
        private const double ProportionBeforePenaltyGender = 0.4;
        private const double ProportionBeforePenaltyAge = 0.25;
        private const double ScoreGrowRate = 0.1;

        private const double GenderDialogueThreshold = -10;
        private const double AgeDialogueThreshold = -15;

        private const double GenderNormalRecovery = -2;
        private const double GenderIgnoreRecovery = -5;

        private float _update = 0.0f;

        private void RecalculateMentalState(Dictionary<GameObject, NPCInfo> npcs)
        {
            var femaleProportion = npcs.Where(npc => npc.Value.Attributes.gender == NPCAttributes.Gender.FEMALE)
                .Sum(npc => (1.0 / npcs.Count));
            
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
                        npc.Value.MentalState.StaffState = StaffMentalState.State.NORMAL;
                    }
                }

                // Update AgeDiversity
                var similarAge = from n in npcs.Values
                    where n.Attributes.age > (npc.Value.Attributes.age - 15) && n.Attributes.age < (npc.Value.Attributes.age + 15)
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
                        npc.Value.MentalState.StaffState = StaffMentalState.State.NORMAL;
                    }
                }

                Debug.Log("Updated NPC " + npc.Value.Attributes.npcName + " genderDiversity = " +
                          npc.Value.MentalState.GenderDiversityScore + ", ageDiversity = " +
                          npc.Value.MentalState.AgeDiversityScore + ", state = "+npc.Value.MentalState.StaffState);

                // Check if we need to take action
                if (npc.Value.MentalState.GenderDiversityScore < GenderDialogueThreshold)
                {
                    if (npc.Value.MentalState.StaffState == StaffMentalState.State.READY_TO_LEAVE ||
                        npc.Value.MentalState.GenderDiversityScore < GenderDialogueThreshold*3) // special case if dialogue ignored?
                    {
                        // TODO: Leave company!
                        
                    }
                    else
                    {
                        Debug.Log("Throwing dialogue!");
                        var dialogue = GenerateGenderDialogue(npc.Value);
                        // Pop dialogue
                        npc.Key.GetComponent<NPCBehaviour>().ShowGenericNotification(delegate
                        {
                            ProjectManager.Instance.PauseProject();
                            DialogueManager.Instance.StartDialogue(dialogue);
                        });
                    }
                    
                }
                else if (npc.Value.MentalState.AgeDiversityScore < AgeDialogueThreshold)
                {
                    // TODO!
                }
            }

            if (femaleProportion < 0.001 || femaleProportion > 0.999) // getting around float accuracy errors
            {
                // TODO: Special Case for 0 of each gender
            }
        }

        private Dialogue GenerateGenderDialogue(NPCInfo npc)
        {
            string sentence;
            StaffMentalState.State nextState;
            string[] choices;
            switch (npc.MentalState.StaffState)
            {
                case StaffMentalState.State.NORMAL:
                    sentence = npc.Attributes.npcName + " thinks the office is boring. "+GetPronoun(npc)+" feels out of place.\n" +
                               "Hire some like-minded coworkers to improve "+GetPronoun(npc).ToLower()+" mood.";
                    nextState = StaffMentalState.State.ANNOYED;
                    choices = new string[]
                    {
                        "OK",
                    };
                    break;
                case StaffMentalState.State.ANNOYED:
                    sentence = npc.Attributes.npcName + " doesn't know many people here. " + GetPronoun(npc) +
                               " feels lonely.\n" +
                               "Encourage your employees to get to know " + GetPronoun(npc).ToLower() +
                               " by hosting a pizza party.";
                    nextState = StaffMentalState.State.ABOUT_TO_LEAVE;
                    choices = new string[]
                    {
                        "Throw Party",
                        "Ignore"
                    };
                    break;
                case StaffMentalState.State.ABOUT_TO_LEAVE:
                    sentence = npc.Attributes.npcName + " feels excluded by her coworkers.\n" +
                               "Host a team-building event to improve workspace culture.";
                    nextState = StaffMentalState.State.READY_TO_LEAVE;
                    choices = new string[]
                    {
                        "Host team-building event",
                        "Ignore"
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", npc.MentalState.StaffState, null);
            }
            return new Dialogue
            {
                Sentences = new Sentence[]{
                    new Sentence(){
                        // icon = TODO: Get Icon somehow!
                        Title = npc.Attributes.npcName,
                        sentenceLine = sentence,
                        sentenceChoices = choices,
                        sentenceChoiceActions = new UnityAction[]{
                            delegate()
                            {
                                npc.MentalState.GenderDiversityScore = GenderNormalRecovery;
                                npc.MentalState.StaffState = nextState;
                                ProjectManager.Instance.ResumeProject();
                            },
                            delegate()
                            {
                                npc.MentalState.GenderDiversityScore = GenderIgnoreRecovery;
                                npc.MentalState.StaffState = nextState;
                                ProjectManager.Instance.ResumeProject();
                            },
                        },
                    }
                }	
            };
        }

        private void Update()
        {
            _update += Time.deltaTime;
            if (!(_update > 5.0f) || ProjectManager.Instance.IsPaused()) return;
            // run every 5 seconds
            _update = 0.0f;
            RecalculateMentalState(NPCController.Instance.NpcInstances);
            Debug.Log("Update");
        }

        private static string GetPronoun(NPCInfo npc)
        {
            switch (npc.Attributes.gender)
            {
                case NPCAttributes.Gender.MALE:
                    return "He";
                case NPCAttributes.Gender.FEMALE:
                    return "She";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}