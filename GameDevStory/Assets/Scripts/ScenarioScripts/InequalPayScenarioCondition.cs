using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueScripts;
using NPCScripts;

public class InequalPayScenarioCondition : AScenarioCondition
{
    public override double getProbability()
    {

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

		if(femaleCount == 0 || maleCount == 0){
			return 0;
		}

		float maleAvg = malePaySum/maleCount;
		float femaleAvg = femalePaySum/femaleCount;

        if(Scenario.getActive() == false && DialogueManager.Instance.GetQueueSize() == 0 && !ProjectManager.Instance.IsPaused()
		 && GameManager.Instance.getBalance() > 200 && ProjectManager.Instance.GetCurrentProject().getDifficulty().Equals(ProjectDifficulty.Hard) && femaleAvg < 0.8 * maleAvg){
			return 0.005;
		}else{
			return 0;
		}
    }
}
