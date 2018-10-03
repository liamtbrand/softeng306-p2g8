using System.Collections;
using System.Collections.Generic;
using DialogueScripts;
using UnityEngine;

public class ConferenceScenarioCondition : AScenarioCondition
{
    public override double getProbability()
    {
        if(Scenario.getActive() == false && DialogueManager.Instance.GetQueueSize() == 0){
			return 0.01;
		}else{
			return 0;
		}
    }
}
