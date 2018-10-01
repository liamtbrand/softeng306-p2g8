using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : Singleton<ProjectManager> {

	private ProjectTimer timerScript;
	private ProjectDisplayManager displayScript;
	public GameObject projectMenu;
	
	Dictionary<string,Project> projects;
	private double profit;

	void Start () {
		//timerScript = GetComponent<ProjectTimer> ();
		//timerScript.enabled = false; 
		projects = ProjectCreator.Instance.InitialiseProjects();
	}

	// Shows the project picker
	public void PickProject () {
		projectMenu.SetActive(true);
		displayScript = GetComponent <ProjectDisplayManager>();
		foreach(KeyValuePair<string, Project> entry in projects)
		{
			displayScript.AddNewProject(
				entry.Value.getTitle(), 
				entry.Value.getCompany(), 
				entry.Value.getDescription(), 
				entry.Value.getStats(), 
				entry.Value.getEnabled());
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