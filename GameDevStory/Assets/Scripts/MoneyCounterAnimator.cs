using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoneyCounterAnimator : MonoBehaviour {
	
	public Text MoneyCounter;
	public double Target { private get; set; }
	private const double BankruptcyLimit = -250;

	// Update is called once per frame
	void Update ()
	{
		var currentValue = Convert.ToDouble(MoneyCounter.text.Replace("$", ""));

		if (currentValue < BankruptcyLimit)
		{
			Destroy(GameManager.Instance);
			SceneManager.LoadScene("Loss");
		}

		//Debug.Log("Difference: "+Math.Abs(currentValue - Target));
		
		var stepSize = 1.0;
		if (Math.Abs(currentValue - Target) > 200)
		{
			stepSize = 5;
		} else if (Math.Abs(currentValue - Target) > 10)
		{
			stepSize = 2;
		} else if (Math.Abs(currentValue - Target) > 0.1)
		{
			stepSize = 0.1;
		} else
		{
			MoneyCounter.text = "$"+(Target);
			return;
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
