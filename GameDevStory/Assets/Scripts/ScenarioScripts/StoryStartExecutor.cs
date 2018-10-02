using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueScripts;
using UnityEngine.Events;

public class StoryStartExecutor : AScenarioExecutor
{

	public Dialogue dialogue1;
	public Dialogue dialogue2;
	public Dialogue dialogue3;
	public Dialogue dialogue4;
	public Dialogue dialogue5;

    public override void execute()
    {
	    var completeDialogue = new Dialogue{
		    Sentences = new[]{
			    new Sentence(){
				    Title = "Pick a project",
				    sentenceLine = "In order to start your journey, pick a project for your staff to start working on!",
				    sentenceChoices = new string[]{
					    "Pick Project"
				    },
				    sentenceChoiceActions = new UnityAction[]{
					    StartProject
				    },
			    }
		    }
	    };
        Debug.Log("Starting story start executor");
		DialogueManager.Instance.StartDialogue(dialogue1);
		DialogueManager.Instance.QueueDialogue(dialogue2);
		DialogueManager.Instance.QueueDialogue(dialogue3);
		DialogueManager.Instance.QueueDialogue(dialogue4);
		DialogueManager.Instance.QueueDialogue(dialogue5);
	    DialogueManager.Instance.QueueDialogue(completeDialogue);
    }

	private void StartProject()
	{
		ProjectManager.Instance.PickProject();
	}
}
