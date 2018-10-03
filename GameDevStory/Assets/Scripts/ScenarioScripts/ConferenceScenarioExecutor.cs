using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueScripts;

public class ConferenceScenarioExecutor : AScenarioExecutor
{
	public Dialogue dialogue2;

    public override void execute()
    {

		var dialogue1 = new Dialogue{
			Sentences = new Sentence[]{
				new Sentence(){
					Title = "Jayne Hustleson",
				    sentenceLine = "Hi, I am speaking at a the DivDevs conference tonight. It would mean a lot if you drop by and listen. You may learn a lot! Are you coming along?",
				    sentenceChoices = new string[]{
					    "Sure thing!",
						"Sorry I am sick that day"
				    },
				    sentenceChoiceActions = new UnityAction[]{
					    YesChoice,
						NoChoice
				    },
				}
			}	
		};
        
		DialogueManager.Instance.StartDialogue(dialogue1);
		
    }

	public void YesChoice(){
		StartCoroutine(WaitThenQueueDialogue());
		DialogueManager.Instance.QueueDialogue(dialogue2);
	}

	public IEnumerator WaitThenQueueDialogue(){
		GameManager.Instance.fadeToBlack();
		yield return new WaitForSeconds(2);
	}

	public void NoChoice(){
	}

}
