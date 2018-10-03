using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectTimer : MonoBehaviour {

	public GameObject progressPanel;
	public Slider progressBar;
	public Text progressText;
	private ProjectManager progressScript;
	public Scenario[] scenarioArray;
	private float maxTime = 10f;
	private float timer;
	private float currentTime;
	private bool paused = false;

	// Set up timer
	void OnEnable ()
	{
		progressScript = GetComponent<ProjectManager> ();
		progressPanel.SetActive(true);
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
					Pause(scenario);
					scenario.StartScenario();
				}
			}
			
			// Decrement timer
			timer -= Time.deltaTime;
			currentTime = timer;
		
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

	public void Pause(Scenario scenario)
	{
		paused = true;
		currentTime = timer;
	}

	public void Resume()
	{
		paused = false;
	}
}
