using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueScripts;

public class ConferenceScenarioExecutor : AScenarioExecutor
{

	public Dialogue dialogue1;
	public Dialogue dialogue2;
    public override void execute()
    {
        
		DialogueManager.Instance.StartDialogue(dialogue1);
		GameManager.Instance.fadeToBlack();
		DialogueManager.Instance.QueueDialogue(dialogue2);
		
    }
}
