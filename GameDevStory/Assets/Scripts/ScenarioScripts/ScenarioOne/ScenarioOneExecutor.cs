using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueScripts;
using UnityEngine.Events;

public class ScenarioOneExecutor : AScenarioExecutor
{

	public Dialogue dialogue1;
	public Dialogue dialogue2;

	public override void execute()
	{
		var completeDialogue = new Dialogue{
			Sentences = new[]{
				new Sentence(){
					Title = "Choose an action",
					sentenceLine = "How do you respond to this scenario?",
					sentenceChoices = new string[]{
						"Action A",
						"Action B"
					},
					sentenceChoiceActions = new UnityAction[]{
						ResumeProject,
						ResumeProject
					},
				}
			}
		};
		Debug.Log("Starting scenario one executor");
		DialogueManager.Instance.StartDialogue(dialogue1);
		DialogueManager.Instance.QueueDialogue(dialogue2);
		DialogueManager.Instance.QueueDialogue(completeDialogue);
	}

	private void ResumeProject()
	{
		ProjectManager.Instance.ResumeProject();
	}
}
