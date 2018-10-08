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
	private float currentTime;
	public bool paused = false;
    private int bugsCreated = 0;
    private int bugsSquashed = 0;
    private float bugProbability = 0.01f; //TODO: link this up with diversity score or some other "quality" attribute

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
					Pause(scenario);
					scenario.StartScenario();
				}
			}

            // Send out bugs during projects
            if (Random.Range(0.0f, 1.0f) < bugProbability)
            {
                bugsCreated++;
                NPCController.Instance.ShowBug(IncrementBugsSquashed, DecrementBugsCreated);
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

	// Pauses the timer
	public void Pause(Scenario scenario)
	{
		paused = true;
		currentTime = timer;
	}

	// Resumes the timer
	public void Resume()
	{
		paused = false;
	}

    public int GetBugsCreated()
    {
        return bugsCreated;
    }

    public int GetBugsSquashed()
    {
        return bugsSquashed;
    }

    private void IncrementBugsSquashed()
    {
        Debug.Log("Bug Squashed!");
        bugsSquashed++;
    }

    private void DecrementBugsCreated()
    {
        Debug.Log("Failed to send bug!");
        bugsCreated--;
    }
}
