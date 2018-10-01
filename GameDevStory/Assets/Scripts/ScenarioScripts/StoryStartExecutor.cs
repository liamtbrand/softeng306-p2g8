using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueScripts;

public class StoryStartExecutor : AScenarioExecutor
{

	public Dialogue dialogue1;
	public Dialogue dialogue2;

    public override void execute()
    {
        Debug.Log("Starting story start executor");
		DialogueManager.Instance.StartDialogue(dialogue1);
		DialogueManager.Instance.QueueDialogue(dialogue2);

    }
}
