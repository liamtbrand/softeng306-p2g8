﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles the timeline of a project
public class ProjectTimer : MonoBehaviour {

	public GameObject progressPanel;
	public Slider progressBar;
	public Text progressText;
	public Text projectText;
	private ProjectManager progressScript;
	private Project currentProject;
	public Scenario[] scenarioArray;
	private float timeMultiplier = 1.5f;
	private float maxTime;
	private float timer;
	public bool paused = false;
    private int bugsCreated;
    private int bugsSquashed;
    private float bugProbability;

	// Set up timer
	void OnEnable ()
	{
		// Show the progress bar
		progressScript = GetComponent<ProjectManager> ();
		currentProject = progressScript.GetCurrentProject();
		progressPanel.SetActive(true);
		paused = false;

		// Set the timer length depending on project length
		maxTime = currentProject.getLength()*timeMultiplier;
		Debug.Log("Max Time: " + maxTime);
		timer = maxTime;
		progressBar.value = 0;

        // initialise bug variables
        bugsSquashed = 0;
        bugsCreated = 0;

        // adjust number of bugs that will show based on difficulty of project
        ProjectDifficulty difficulty = currentProject.getDifficulty();
        if (difficulty == ProjectDifficulty.Tutorial)
            bugProbability = 0; // no bugs on first level
        else if (difficulty == ProjectDifficulty.Easy)
            bugProbability = 0.005f;
        else if (difficulty == ProjectDifficulty.Medium)
            bugProbability = 0.0075f;
        else
            bugProbability = 0.01f;
            
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!paused)
		{

            // Fire scenarios during projects
            foreach (Scenario scenario in scenarioArray)
            {
                if (scenario.getStatus() == ScenarioStatus.INCOMPLETE &&
                    Scenario.getActive() == false
                    && Random.Range(0.0f, 1.0f) < scenario.GetScenarioProbability())
                {
                    Pause();
                    scenario.StartScenario();
                }
            }

            // Send out bugs during projects unless there are less than 3 seconds remaining
            if (Random.Range(0.0f, 1.0f) < bugProbability && timer >= 3)
            {
                bugsCreated++;
                NPCController.Instance.ShowBug(() => bugsSquashed++);
            }

            // Decrement timer
            timer -= Time.deltaTime;
		
			// Update progress bar
			float progress = currentProject.getLength()*(maxTime-timer)/maxTime;
			progressText.text = "Day: " + progress.ToString("f0") + "/" + currentProject.getLength();
			progressBar.value = (maxTime-timer)/maxTime;

			// Project completed stop timer
			if (timer <= 0) {
				progressText.text = "100%";
				progressPanel.SetActive(false);
				progressScript.CompletedProject();
			}
        }
	}

	// Pauses the timer
	public void Pause()
	{
		paused = true;
	}

	// Resumes the timer
	public void Resume()
	{
		paused = false;
	}

	public void DisplayCurrentProject(string project)
	{
		projectText.text = project;
	}

    public int GetBugsMissed()
    {
        return bugsCreated - bugsSquashed;
    }
}
