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

	// Handle project selection
	public void PickProject () {
		// Show project menu
		projectMenu.SetActive(true);

		// Initialise list of projects
		displayScript = GetComponent <ProjectDisplayManager>();
		if (projects == null) {
			projects = ProjectCreator.Instance.InitialiseProjects();
		}

		// Display projects
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
		displayScript.ClearAllProjects();

		// Start project progress timer
		timerScript.enabled = true;

		// Wait till a project is completed
		Invoke("CompletedProject", 10f);
	}

	void CompletedProject()
 	{
		// Change project object to completed
		Project projectObject = projects[selectedProject];
		projectObject.setCompleted(true);
		projects[selectedProject] = projectObject;

		// Calculate project profit
		double profit = CalculateProjectProfit(selectedProject);

		// Calculate project stars
		int stars = CalculateProjectStars(selectedProject);

		// Show project completion display
		displayScript.ProjectCompleted(profit,stars);

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
		return 100.00;
	}

}