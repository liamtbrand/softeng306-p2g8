using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectDisplayManager : MonoBehaviour
{
    public GameObject ProjectSelectionContent;
    public GameObject ProjectEntryPrefab;
    public GameObject ProjectEntryTitlePrefab;
    
    protected ProjectDisplayManager () {} // enforces singleton use

    public void ClearAllProjects()
    {
        foreach (Transform child in ProjectSelectionContent.transform) {
            GameObject.Destroy(child.gameObject);
        }
        // Add back the title!
        Instantiate(ProjectEntryTitlePrefab, Vector3.zero, Quaternion.identity, ProjectSelectionContent.transform);
    }

    // This is called back when the project is selected
    public delegate void ProjectSelectedCallBack();

    public void AddNewProject(string title, string company, string description, string stats, bool selectable,
        ProjectSelectedCallBack callback)
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
            button[0].onClick.AddListener(delegate { callback(); }); // set button callback
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
    
}
