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
    private float bugProbability = 0.005f; //TODO: link this up with project difficulty

	// Set up timer
	void OnEnable ()
	{
		// Show the progress bar
		progressScript = GetComponent<ProjectManager> ();
		currentProject = progressScript.GetCurrentProject();
		progressPanel.SetActive(true);

		// Set the timer length depending on project length
		maxTime = currentProject.getLength()*timeMultiplier;
		timer = maxTime;
		progressBar.value = 0;

        // initialise bug variables
        bugsSquashed = 0;
        bugsCreated = 0;
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
