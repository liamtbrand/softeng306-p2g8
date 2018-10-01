using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStartCondition : AScenarioCondition {

	public override double getProbability()
    {
        Debug.Log("Returning 1 for story start condition");
        return 1;
        
    }

}
