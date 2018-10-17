using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueScripts;

public class InequalPayScenarioExecutor : AScenarioExecutor
{

    public Sprite anonSprite;
    public Sprite lawyerSprite;

    private float payDifferential;

    public override void execute()
    {

        payDifferential = CalculateAverageDifference();

        ProjectManager.Instance.PauseProject();

        var dialogue1 = new Dialogue
		{
			Sentences = new Sentence[]{
                new Sentence(){
                    icon = anonSprite,
					Title = "Anonymous",
				    sentenceLine = "It has come to my attention that the women in this office are being paid significantly less than their male counterparts.",
                },
                new Sentence(){
                    icon = anonSprite,
					Title = "Anonymous",
				    sentenceLine = "The women in this office do just as much work as the men, and the pay difference is wholly unjustified. If something isn't done we will consider taking legal action.",
                },
				new Sentence(){
					icon = anonSprite,
					Title = "Anonymous",
				    sentenceLine = "What do you plan to do about this?",
				    sentenceChoices = new string[]{
					    "We will mend this issue immediately!. (Costs $" + payDifferential.ToString() + ")",
						"Sorry but money is tight right now."
				    },
				    sentenceChoiceActions = new UnityAction[]{
					    EqualisePayChoice,
						NoAction
				    },
				}
			}	
		};

        DialogueManager.Instance.StartDialogue(dialogue1);

    }

    

    public void EqualisePayChoice(){
        ProjectManager.Instance.ResumeProject();
        GameManager.Instance.changeBalance(-payDifferential);
        NPCFactory.Instance.FemalePayMultiplier = 1;
    }

    public void NoAction(){

        StartCoroutine(WaitThenQueueDialogue());

        var dialogue2 = new Dialogue(){
            Sentences = new Sentence[]{
                new Sentence(){
                    icon = lawyerSprite,
					Title = "Lawyer",
				    sentenceLine = "Good afternoon..",
                },
                new Sentence(){
                    icon = lawyerSprite,
					Title = "Lawyer",
				    sentenceLine = "We have received complaints that your company has been using unfair pay practices.",
                },
                new Sentence(){
                    icon = lawyerSprite,
					Title = "Lawyer",
				    sentenceLine = "On behalf of the LittleTown State Commmission, you are being sued for gender pay discrimination.",
                },
                new Sentence(){
                   icon = lawyerSprite,
					Title = "Lawyer",
				    sentenceLine = "We will see you in court...", 
                    sentenceChoices = new string[]{
					    "Continue"
				    },
				    sentenceChoiceActions = new UnityAction[]{
					    FinishLawsuit
				    },
                }
            }
        };

        DialogueManager.Instance.QueueDialogue(dialogue2);

    }

    public void FinishLawsuit() {

        var dialogue3 = new Dialogue(){
            Sentences = new Sentence[]{
                new Sentence(){
                    sentenceLine = "The lawsuit was lost, and company had to pay $500 in remediations, $" +  payDifferential +
                     " to even current pay gaps, and use fair pay and hiring practices in the future"
                }
            }
        };

		Debug.Log("UNFADING NOW");
		GameManager.Instance.Unfade();

        DialogueManager.Instance.QueueDialogue(dialogue3);

        GameManager.Instance.changeBalance(-500);

        EqualisePayChoice();
	}

    private int CalculateAverageDifference(){
        var malePaySum = 0f;
		var maleCount = 0f;
		var femalePaySum = 0f;
		var femaleCount = 0f;

		foreach( var npcPair in NPCController.Instance.NpcInstances){
			if(npcPair.Value.Attributes.gender.Equals(NPCAttributes.Gender.MALE)){
				malePaySum += npcPair.Value.Attributes.ammountPaidFor;
				maleCount++;
			}else{
				femalePaySum += npcPair.Value.Attributes.ammountPaidFor;
				femaleCount++;
			}
		}

		float maleAvg = malePaySum/maleCount;
		float femaleAvg = femalePaySum/femaleCount;

        return Convert.ToInt32((maleAvg - femaleAvg) * femaleCount);
    }

    public IEnumerator WaitThenQueueDialogue(){
        Debug.Log("FADING NOW");
        GameManager.Instance.fadeToBlack();
        yield return new WaitForSeconds(2);
	}

}

