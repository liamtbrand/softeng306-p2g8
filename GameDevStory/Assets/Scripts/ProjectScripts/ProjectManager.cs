﻿using System.Collections;
using System.Collections.Generic;
using ProjectScripts;
using UnityEngine;
using UnityEngine.UI;

// Handles projects
public class ProjectManager : Singleton<ProjectManager> {

	private ProjectTimer timerScript;
	private ProjectDisplayManager displayScript;
	public GameObject projectMenu;

	private static Dictionary<string,Project> projects;
	private string  selectedProject;

	void Start () {
		// Stop the timer from running
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

	public bool IsPaused()
	{
		return timerScript.paused;
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
		timerScript.DisplayCurrentProject(selectedProject);

		// If you want to test the pausing functionality (pauses after 3s)
		//Invoke("PauseProject", 5f);
	}

	// Pauses the work on a project
	public void PauseProject()
	{
		timerScript.Pause();
		// If you want to test the resume functionality (resumes after 5s)
		//Invoke("ResumeProject", 5f);
	}
	
	// Resumes the work on a project
	public void ResumeProject()
	{
		timerScript.Resume();
	}

	// Handles the completion of a project
	public void CompletedProject()
 	{
		// Reset timer
		timerScript.enabled = false;

        // determine bug statistics 
        int bugsMissed = timerScript.GetBugsCreated() - timerScript.GetBugsSquashed();
		 
		// Update project menu
		UpdateProjectMenu(selectedProject);

		// Calculate project profit
		double profit = CalculateProjectProfit(selectedProject, bugsMissed);

		// Calculate project stars
		//int stars = CalculateProjectStars(selectedProject);

		// Get project feedback
		string feedback = GetProjectFeedback(selectedProject);

		// Show project completion display
		displayScript.ProjectCompleted(profit,feedback, bugsMissed, bugsMissed * 10);

		// Add to total profits
		 GameManager.Instance.changeBalance(profit);
	 }

	// Calculates the performance of a project
	string GetProjectFeedback (string project)
	{
		// TODO: Get feedback based on diversity
		return "Your perspective was too narrow. Try...";
	}

	// Calculates the performance of a project
	int CalculateProjectStars (string project)
	{
		// TODO: Calculate stars based on diversity
		return 3;
	}

	// Calculates the profit from a project
	double CalculateProjectProfit (string project, int bugsMissed) 
	{
		// TODO: Calculate project profit based on diversity and don't hardcode bug penalty
		return 100.00 - 10 * bugsMissed;
	}

	// Updates the project menu
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