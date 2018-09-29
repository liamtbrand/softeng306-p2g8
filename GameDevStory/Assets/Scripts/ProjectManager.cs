using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour {

	private ProjectTimer timerScript;
	private GameManager gameScript;
	public Button startProject;

	private double profit;
	public GameObject[] projects;		// Array of project prefabs.

	void Start () {
		gameScript = GetComponent<GameManager> ();
		timerScript = GetComponent<ProjectTimer> ();
		timerScript.enabled = false;
	}

	// Starts a project
	public void StartProject ()
	{
		// Start project timer
		timerScript.enabled = true;
		
		// Calculate project profit
		profit = CalculateProjectProfit ();

		// Add to total profits
		gameScript.changeBalance(profit);
	}

	double CalculateProjectProfit () {
		// int femaleNPC = NPCScript.getFemaleWorkers();
		// int maleNPC = NPCScript.getMaleWorkers();
		// calculate diversity???
		return 0.0;
	}

}