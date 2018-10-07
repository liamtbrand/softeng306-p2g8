using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles the timeline of a project
public class ProjectTimer : MonoBehaviour {

	public GameObject progressPanel;
	public Slider progressBar;
	public Text progressText;
	private ProjectManager progressScript;
	public Scenario[] scenarioArray;
	private float maxTime = 10f;
	private float timer;
	private bool paused = false;

	// Set up timer
	void OnEnable ()
	{
		// Show the progress bar
		progressScript = GetComponent<ProjectManager> ();
		progressPanel.SetActive(true);

		// Set the timer values
		timer = maxTime;
		progressBar.value = 0;
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
			
			// Decrement timer
			timer -= Time.deltaTime;
		
			// Update progress bar
			float progress = (maxTime-timer)/maxTime*100;
			progressText.text = progress.ToString("f0")+"%";
			progressBar.value = progress/100;

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
}
