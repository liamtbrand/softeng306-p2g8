using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterAnimator : MonoBehaviour {
	
	public Text MoneyCounter;
	public double Target { private get; set; }

	// Update is called once per frame
	void Update ()
	{
		var currentValue = Convert.ToDouble(MoneyCounter.text.Replace("$", ""));

		//Debug.Log("Difference: "+Math.Abs(currentValue - Target));
		
		var stepSize = 1.0;
		if (Math.Abs(currentValue - Target) > 500)
		{
			stepSize = 5;
		} else if (Math.Abs(currentValue - Target) > 100)
		{
			stepSize = 2;
		} else if (Math.Abs(currentValue - Target) > 0.1)
		{
			stepSize = 0.1;
		} else
		{
			stepSize = 0.005;
		}
		
		if (currentValue < Target)
		{
			MoneyCounter.text = "$"+(currentValue + stepSize);
		} else if (currentValue > Target)
		{
			MoneyCounter.text = "$"+(currentValue - stepSize).ToString();
		}
	}
}
