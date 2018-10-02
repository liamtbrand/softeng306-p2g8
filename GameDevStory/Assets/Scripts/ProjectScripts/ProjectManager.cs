using System.Collections;
using System.Collections.Generic;
using ProjectScripts;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : Singleton<ProjectManager> {

	private ProjectTimer timerScript;
	private ProjectDisplayManager displayScript;
	public GameObject projectMenu;

	private static Dictionary<string,Project> projects;
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
		Invoke("CompletedProject", 10.5f);
	}

	void CompletedProject()
 	{
		// Reset timer
		timerScript.enabled = false;
		 
		// Update project menu
		UpdateProjectMenu(selectedProject);

		// Calculate project profit
		double profit = CalculateProjectProfit(selectedProject);

		// Calculate project stars
		int stars = CalculateProjectStars(selectedProject);

		// Show project completion display
		displayScript.ProjectCompleted(profit,stars);

		// Add to total profits
		//gameScript.changeBalance(profit);
 	}

	int CalculateProjectStars (string project)
	{
		// TODO: Calculate stars based on diversity
		return 3;
	}

	double CalculateProjectProfit (string project) 
	{
		// TODO: Calculate project profit based on diversity
		return 100.00;
	}

	void UpdateProjectMenu(string project)
	{
		ProjectDifficulty difficulty = projects[selectedProject].getDifficulty();
		projects.Remove(project);

		// Check if finished all projects of the same difficulty
		bool finishedAllDifficulty = true;
		foreach(KeyValuePair<string, Project> entry in projects)
		{
			if (entry.Value.getDifficulty() == difficulty)
			{
				finishedAllDifficulty = false;
			}
		}

		// Unlock next difficulty level
		if (finishedAllDifficulty)
		{
			List<string> keys = new List<string>(projects.Keys);
			foreach(var entry in keys)
			{
				Project unlockedProject = projects[entry];
				
				// Unlock all easy levels
				if (difficulty == ProjectDifficulty.Tutorial && 
				    unlockedProject.getDifficulty() == ProjectDifficulty.Easy)
				{
					unlockedProject.setEnabled(true);
					projects[entry] = unlockedProject;
				}
				
				// Unlock all medium levels
				if (difficulty == ProjectDifficulty.Easy && 
				    unlockedProject.getDifficulty() == ProjectDifficulty.Medium)
				{
					unlockedProject.setEnabled(true);
					projects[entry] = unlockedProject;
				}

				// Unlock all hard levels
				if (difficulty == ProjectDifficulty.Medium &&
				    unlockedProject.getDifficulty() == ProjectDifficulty.Hard)
				{
					unlockedProject.setEnabled(true);
					projects[entry] = unlockedProject;
				}
			}
		}
	}

}