using System;
using UnityEngine;
using UnityEngine.UI;

// Handles the Project UI
namespace ProjectScripts
{
    public class ProjectDisplayManager : MonoBehaviour
    {
        public GameObject ProjectSelectionContent;
        public GameObject ProjectEntryPrefab;
        public GameObject ProjectTitlePrefab;

        public GameObject ProjectCompletePanel;
        public GameObject ProjectCompleteContent;
        public GameObject Star;
        public GameObject HollowStar;
        public Text ProfitText;
        public Text BugStatsText;
    
        protected ProjectDisplayManager () {} // enforces singleton use

        // Removes all projects from the project container
        public void ClearAllProjects()
        {
            foreach (Transform child in ProjectSelectionContent.transform) {
                GameObject.Destroy(child.gameObject);
            }
            Instantiate(ProjectTitlePrefab, Vector3.zero, Quaternion.identity, ProjectSelectionContent.transform);
        }

        // Adds a project to the project menu
        public void AddNewProject(string title, string company, string description, string stats, bool selectable,
            Action<string> callback)
        {
            var projectPrefab = Instantiate(ProjectEntryPrefab, Vector3.zero, Quaternion.identity, ProjectSelectionContent.transform);
            var text = projectPrefab.GetComponentsInChildren<Text>();
            text[0].text = title;
            text[1].text = company;
            text[2].text = description;
            text[3].text = stats;
        
            var button = projectPrefab.GetComponentsInChildren<Button>(true); // get inactive children too!
            if (selectable)
            {
                button[0].onClick.AddListener(delegate { callback(title); }); // set button callback
            }
            else
            {
                button[0].interactable = false;
            }
        }

        // overload for unselectable
        public void AddNewProject(string title, string company, string description, string stats, bool selectable)
        {
            AddNewProject(title, company, description, stats, selectable, delegate {  });
        }

        // Displays the project completion display
        public void ProjectCompleted(double profit, int stars, int bugsMissed, int bugPenalty)
        {
            ProjectCompletePanel.SetActive(true);
            ProfitText.text = "$" + profit.ToString("f2");
        
            float offset = 0.3f;
            float xPos = -0.3f;
            float yPos = 0.05f;
            int maxStars = 3;
            int hollowStars = maxStars - stars;

            // Instantiate stars
            for (int j=0; j<stars; j++) {
                Instantiate(Star, new Vector3(xPos,yPos,0f), Quaternion.identity,ProjectCompleteContent.transform);
                xPos+=offset;
            }

            // Instantiate hollow stars
            for (int i=0; i<hollowStars; i++) {
                Instantiate(HollowStar, new Vector3(xPos,yPos,0f), Quaternion.identity,ProjectCompleteContent.transform);
                xPos+=offset;
            }

            //BugStatsText.text = "Bugs Missed: " + bugsMissed + " (-$" + bugPenalty + ")";
        }

        // Closes the project completion display
        public void CloseProjectCompleted()
        {
            ProjectCompletePanel.SetActive(false);
            foreach (Transform child in ProjectCompleteContent.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    
    }
}
