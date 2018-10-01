using System;
using UnityEngine;
using UnityEngine.UI;

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
    
        protected ProjectDisplayManager () {} // enforces singleton use

        public void ClearAllProjects()
        {
            foreach (Transform child in ProjectSelectionContent.transform) {
                GameObject.Destroy(child.gameObject);
            }
            Instantiate(ProjectTitlePrefab, Vector3.zero, Quaternion.identity, ProjectSelectionContent.transform);
        }

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
                button[0].gameObject.SetActive(false);
                button[1].gameObject.SetActive(true);
            }
        }

        // overload for unselectable
        public void AddNewProject(string title, string company, string description, string stats, bool selectable)
        {
            AddNewProject(title, company, description, stats, selectable, delegate {  });
        }

        public void ProjectCompleted(double profit, int stars)
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
        }

        public void CloseProjectCompleted()
        {
            ProjectCompletePanel.SetActive(false);
            foreach (Transform child in ProjectCompleteContent.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    
    }
}
