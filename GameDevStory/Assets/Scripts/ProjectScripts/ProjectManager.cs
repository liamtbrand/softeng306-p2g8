using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : Singleton<ProjectManager> {

	private ProjectTimer timerScript;
	public Button startProject;
	public GameObject projectSelection;
	private ProjectSelector projectScript;

	private double profit;
	public GameObject[] projects;		// Array of project prefabs.

	void Start () {
		//timerScript = GetComponent<ProjectTimer> ();
		//timerScript.enabled = false; 
	}

	// Shows the project picker
	public void PickProject () {
		projectSelection.SetActive(true);
		projectScript = GetComponent <ProjectSelector>();
        projectScript.SetupProjectMenu();

		Dictionary<string,Project> projects = ProjectCreator.Instance.InitialiseProjects();
		foreach(KeyValuePair<string, Project> entry in projects)
		{
			Debug.Log(entry.Key);
		}
	}

	// Starts a project
	public void StartProject ()
	{
		// Start project timer
		timerScript = GetComponent<ProjectTimer> ();
		timerScript.enabled = true;
		
		// Calculate project profit
		profit = CalculateProjectProfit ();

		// Add to total profits
		//gameScript.changeBalance(profit);
	}

	double CalculateProjectProfit () {
		// int femaleNPC = NPCScript.getFemaleWorkers();
		// int maleNPC = NPCScript.getMaleWorkers();
		// calculate diversity???
		return 1000.00;
	}

}