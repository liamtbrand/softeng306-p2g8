using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPCScripts.StaffStateScripts;
using ProjectScripts;
using UnityEngine;
using UnityEngine.UI;

// Handles projects
public class ProjectManager : Singleton<ProjectManager>
{
    private ProjectTimer timerScript;
    private ProjectDisplayManager displayScript;
    private LoadScene loadScript;
    public GameObject projectMenu;

    private static Dictionary<string, Project> projects;
    private string selectedProject;
    public Button DisplayButton;
    public Button DescriptionCloseButton;
    public Button ProjectCompletedButton;

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
            /* if (GameManager.Instance.getBalance() > 0) {
                // Bought out cut scene
            } else {
                // Bankrupt cut scene
            }*/
            loadScript = GetComponent<LoadScene>();
            loadScript.LoadHighScoreScene();
        }

        // Display projects
        foreach (var entry in projects)
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
    public void StartProject(string project)
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

    // Returns current project object
    public Project GetCurrentProject()
    {
        return projects[selectedProject];
    }

    public void DisplayProjectDescription()
    {
        Project current = projects[selectedProject];
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

        var completedProject = projects[selectedProject];

        // Update project menu
        UpdateProjectMenu(selectedProject);

        var diversityScore = StaffDiversityManager.Instance.DiversityScore;
        // Calculate project profit
        var profit = CalculateProjectProfit(completedProject, bugsMissed, diversityScore);

        // Get project feedback
        var feedback = GetProjectFeedback(selectedProject);

        // Show project completion display
        displayScript.ProjectCompleted(profit, feedback);

        // Add to total profits
        GameManager.Instance.changeBalance(profit);
    }

    // Calculates the performance of a project
    private string GetProjectFeedback(string project)
    {
        // TODO: Get feedback based on diversity
        var builder = new StringBuilder();

        // TODO: Change these strings
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
    double CalculateProjectProfit(Project project, int bugsMissed, double diversityScore)
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
        return (baseValue) * (1 - (diversityScore*0.2)) * (1 - bugPenalty*0.2) * (1 - npcStatPenalty*0.3);
    }

    // Updates the project menu
    void UpdateProjectMenu(string project)
    {
        ProjectDifficulty difficulty = projects[selectedProject].getDifficulty();
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