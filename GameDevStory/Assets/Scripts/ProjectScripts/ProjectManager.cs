using System.Collections;
using System.Collections.Generic;
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
    public GameObject projectMenu;

    private static Dictionary<string, Project> projects;
    private string selectedProject;

    void Start()
    {
        // Stop the timer from running
        timerScript = GetComponent<ProjectTimer>();
        timerScript.enabled = false;
    }

    // Handle project selection
    public void PickProject()
    {
        // Show project menu
        projectMenu.SetActive(true);

        // Initialise list of projects
        displayScript = GetComponent<ProjectDisplayManager>();
        if (projects == null)
        {
            projects = ProjectCreator.Instance.InitialiseProjects();
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

    // Handles the completion of a project
    public void CompletedProject()
    {
        // Reset timer
        timerScript.enabled = false;

        // determine bug statistics 
        var bugsMissed = timerScript.GetBugsCreated() - timerScript.GetBugsSquashed();

        // Update project menu
        UpdateProjectMenu(selectedProject);

        var diversityScore = StaffDiversityManager.Instance.DiversityScore;
        // Calculate project profit
        var profit = CalculateProjectProfit(selectedProject, bugsMissed, diversityScore);

        // Calculate project stars
        //int stars = CalculateProjectStars(selectedProject);

        // Get project feedback
        var feedback = GetProjectFeedback(selectedProject);

        // Show project completion display
        displayScript.ProjectCompleted(profit, feedback, bugsMissed, bugsMissed * 10, diversityScore);

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
            builder.Append("The customer found that your team's perspective was narrow. Maybe a change in the composition" +
                           "of your team could improve this?\n");
        }
        else
        {
            builder.Append("The customer found that your team's perspective was spot on, and helped deliver a very relevant product!\n");
        }

        if (timerScript.GetBugsCreated() - timerScript.GetBugsSquashed() > 3)
        {
            builder.Append("The customer was very disappointed in the amount of bugs they found in your product after they tested it internally.\n");
        } else if (timerScript.GetBugsCreated() - timerScript.GetBugsSquashed() > 0)
        {
            builder.Append("The customer found a few bugs in your product, and was disappointed in this.\n");
        }
        else
        {
            builder.Append("The customer is happy with the functionality of your product, and wasn't able to find any major bugs.\n");
        }

        //builder.Append("Otherwise, the customer was happy with your work on "+project);

        return builder.ToString();
    }

    // Calculates the performance of a project
    int CalculateProjectStars(string project)
    {
        // TODO: Calculate stars based on diversity
        return 3;
    }

    // Calculates the profit from a project
    double CalculateProjectProfit(string project, int bugsMissed, double diversityScore)
    {
        // TODO: Don't hardcode bug penalty
        // DiversityStore DECREASES with INCREASED diversity
        return (100.00 - 10 * bugsMissed) * (1 - (diversityScore*0.2));
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
}