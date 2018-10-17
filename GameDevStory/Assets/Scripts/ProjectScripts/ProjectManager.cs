using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPCScripts.StaffStateScripts;
using ProjectScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Handles projects
public class ProjectManager : Singleton<ProjectManager>
{
    private ProjectTimer timerScript;
    private ProjectDisplayManager displayScript;
    private LoadScene loadScript;
    public GameObject projectMenu;

    public Dictionary<string, Project> projects; // Making non static to fix replayability issues
    public string SelectedProject;
    public Button DisplayButton;
    public Button DescriptionCloseButton;
    public Button ProjectCompletedButton;

    public GameObject[] ObjectsToMonitor;

    void Start()
    {
        // Stop the timer from running
        timerScript = GetComponent<ProjectTimer>();
        timerScript.enabled = false;

        displayScript = GetComponent<ProjectDisplayManager>();
        DisplayButton.onClick.AddListener(DisplayProjectDescription);
        DescriptionCloseButton.onClick.AddListener(CloseProjectDescription);
        ProjectCompletedButton.onClick.AddListener(PickProject);
    }

    // Handle project selection
    public void PickProject()
    {
        // Close project completed panel
        displayScript.CloseProjectCompleted();

        // Show project menu
        projectMenu.SetActive(true);

        // Initialise list of projects
        if (projects == null)
        {
            projects = ProjectCreator.Instance.InitialiseProjects();
        }

        // Load end of game
        if (projects.Count == 0)
        {
            // TODO: change to cutscene
            if (GameManager.Instance.getBalance() > 0) {
                loadScript = GetComponent<LoadScene>();
                loadScript.LoadEndingCutscene();
            } else {
                Destroy(GameManager.Instance);
                SceneManager.LoadScene("Loss");
            }
        }

        // Current number of workers
        int numberOfWorkers = NPCController.Instance.NpcInstances.Count;
        Debug.Log("Number of employees: " + numberOfWorkers);

        // Display projects
        displayScript.ClearAllProjects();
        foreach (var entry in projects)
        {
            if (entry.Value.getMinWorkers() > numberOfWorkers) 
            {
                displayScript.AddNewProject(
                entry.Value.getTitle(),
                entry.Value.getCompany(),
                entry.Value.getDescription(),
                entry.Value.getStats(),
                false,
                StartProject);
            } else 
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
    }

    // Hides the project menu
    public void HideProjectMenu()
    {
        projectMenu.SetActive(false);
    }

    public bool IsPaused()
    {
        return timerScript.paused;
    }

    // Starts a project
    public void StartProject(string project)
    {
        // Remove project menu from display
        SelectedProject = project;
        projectMenu.SetActive(false);
        displayScript.ClearAllProjects();

        // Start project progress timer
        timerScript.enabled = true;
        Debug.Log("Timer script enabled");
        timerScript.DisplayCurrentProject(SelectedProject);

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
        foreach (var obj in ObjectsToMonitor)
        {
            if (obj.activeInHierarchy) return;
        }
        timerScript.Resume();
    }

    // Returns current project object
    public Project GetCurrentProject()
    {
        return projects != null && projects.ContainsKey(SelectedProject) ? projects[SelectedProject] : null;

    }

    public void DisplayProjectDescription()
    {
        var current = projects[SelectedProject];
        displayScript.ProjectDescription(
            current.getTitle(),
            current.getCompany(),
            current.getDescription(),
            current.getStats()
        );
        PauseProject();
    }

    public void CloseProjectDescription()
    {
        displayScript.CloseProjectDescription();
        ResumeProject();
    }

    // Handles the completion of a project
    public void CompletedProject()
    {
        // Reset timer
        timerScript.enabled = false;

        // determine bug statistics 
        var bugsMissed = timerScript.GetBugsMissed();
        Debug.Log("Bugs Missed: " + bugsMissed);

        var completedProject = projects[SelectedProject];

        // Update project menu
        UpdateProjectMenu(SelectedProject);

        var diversityScore = StaffDiversityManager.Instance.DiversityScore;
        // Calculate project profit
        var profit = CalculateProjectProfit(completedProject, bugsMissed, diversityScore);

        // Get project feedback
        var feedback = GetProjectFeedback(SelectedProject);

        // Show project completion display
        displayScript.ProjectCompleted(profit, feedback);

        // Add to total profits
        GameManager.Instance.changeBalance(profit);
    }

    // Calculates the performance of a project
    private string GetProjectFeedback(string project)
    {
        var builder = new StringBuilder();

        if (StaffDiversityManager.Instance.DiversityScore >= 0.5)
        {
            builder.Append("The customer found that your team's perspective was very narrow. You could improve this in" +
                           " the future by making sure your team has a diverse range of perspectives and people.\n");
        } else if (StaffDiversityManager.Instance.DiversityScore >= 0.2)
        {
            builder.Append("The customer found that your team's perspective was narrow. Maybe a change in the composition " +
                           "of your team could improve this?\n");
        }
        else
        {
            builder.Append("The customer found that your team's perspective was spot on, and helped deliver a very relevant product!\n");
        }

        if (timerScript.GetBugsMissed() > 3)
        {
            builder.Append("The customer was very disappointed in the amount of bugs they found in your product after they tested it internally.\n");
        } else if (timerScript.GetBugsMissed() > 0)
        {
            builder.Append("The customer found a few bugs in your product, and was disappointed in this.\n");
        }
        else
        {
            builder.Append("The customer is happy with the functionality of your product, and wasn't able to find any major bugs.\n");
        }

        if (NPCAverageStat() > 75)
        {
            builder.Append("Overall, the customer was happy with the quality of your product.");
        } else if (NPCAverageStat() > 50)
        {
            builder.Append("The customer found the quality of your product to be acceptable");
        } else if (NPCAverageStat() > 25)
        {
            builder.Append(
                "The customer found the quality of your product to be lacking. Perhaps you need to hire more highly skilled staff?");
        }
        else
        {
            builder.Append(
                "The customer found the quality of your product to be poor. You need to hire more highly skilled staff to improve future product quality.");
        }

        //builder.Append("Otherwise, the customer was happy with your work on "+project);

        return builder.ToString();
    }
    
    // Calculates the profit from a project
    int CalculateProjectProfit(Project project, int bugsMissed, double diversityScore)
    {
        // Get base amount based on difficulty
        var baseValue = 0.0;

        var npcStatPenalty = (1 - (NPCAverageStat() / NPCController.Instance.NpcInstances.Count()) / 100);
        
        switch (project.getDifficulty())
        {
            case ProjectDifficulty.Tutorial:
                baseValue = 100.0;
                break;
            case ProjectDifficulty.Easy:
                baseValue = 150.0;
                break;
            case ProjectDifficulty.Medium:
                baseValue = 250.0;
                break;
            case ProjectDifficulty.Hard:
                baseValue = 325.0;
                break;
        }

        var bugPenalty = (bugsMissed > 10) ? 1 : (bugsMissed / 10.0);
        
        // DiversityStore DECREASES with INCREASED diversity
        // BugPenalty INCREASES with more bugs
        return Convert.ToInt32((baseValue) * (1 - (diversityScore*0.2)) * (1 - bugPenalty*0.2) * (1 - npcStatPenalty*0.3));
    }

    // Updates the project menu
    void UpdateProjectMenu(string project)
    {
        ProjectDifficulty difficulty = projects[SelectedProject].getDifficulty();
        projects.Remove(project);

        // Check if finished all projects of the same difficulty
        var finishedAllDifficulty = true;
        foreach (var entry in projects)
        {
            if (entry.Value.getDifficulty() == difficulty)
            {
                finishedAllDifficulty = false;
            }
        }

        // Unlock next difficulty level
        if (finishedAllDifficulty)
        {
            var keys = new List<string>(projects.Keys);
            foreach (var entry in keys)
            {
                var unlockedProject = projects[entry];

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

    double NPCAverageStat()
    {
        var npcsAverageStat = 0.0;
        
        // Calculate staff ability
        foreach (var npcInfo in NPCController.Instance.NpcInstances.Values)
        {
            npcsAverageStat += (npcInfo.Stats.Communication + npcInfo.Stats.Creativity + npcInfo.Stats.Design +
                                npcInfo.Stats.Technical + npcInfo.Stats.Testing)/5.0;
        }

        return npcsAverageStat;
    }
}