using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioOneCondition : AScenarioCondition {

	public override double getProbability()
	{
		Debug.Log("Returning 0.8 for scenario one condition");
		return 0.01;
        
	}

}
