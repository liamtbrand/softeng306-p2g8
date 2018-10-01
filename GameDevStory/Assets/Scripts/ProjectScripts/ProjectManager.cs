using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : Singleton<ProjectManager> {

	private ProjectTimer timerScript;
	private ProjectDisplayManager displayScript;
	public GameObject projectMenu;

	private Dictionary<string,Project> projects;
	private string  selectedProject;

	void Start () {
		timerScript = GetComponent<ProjectTimer> ();
		timerScript.enabled = false; 
	}

	// Shows the project picker
	public void PickProject () {
		projectMenu.SetActive(true);
		displayScript = GetComponent <ProjectDisplayManager>();
		projects = ProjectCreator.Instance.InitialiseProjects();
		foreach(KeyValuePair<string, Project> entry in projects)
		{
			displayScript.AddNewProject(
				entry.Value.getTitle(), 
				entry.Value.getCompany(), 
				entry.Value.getDescription(), 
				entry.Value.getStats(), 
				entry.Value.getEnabled(),
				StartProject);
		}
	}

	// Starts a project
	public void StartProject (string project)
	{
		// Remove project menu from display
		selectedProject = project;
		projectMenu.SetActive(false);

		// Start project progress timer
		timerScript.enabled = true;

		// Wait till a project is completed
		Invoke("ExecuteAfterTime", 10f);
	}

	void CompletedProject()
 	{
		// Calculate project profit
		double profit = CalculateProjectProfit(selectedProject);

		// Show project completion display
		displayScript.ProjectCompleted(profit);

		// Add to total profits
		//gameScript.changeBalance(profit);
 	}

	int CalculateProjectStars(string project)
	{
		// TODO: Calculate stars based on diversity
		return 3;
	}

	double CalculateProjectProfit (string project) 
	{
		// TODO: Calculate project profit based on diversity
		return 1000.00;
	}

}