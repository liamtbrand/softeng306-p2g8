using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenarioCondition : AScenarioCondition {

	public override double getProbability()
    {

        Debug.Log("Test scenario condition checked");

        return 0.5;
    }

}
