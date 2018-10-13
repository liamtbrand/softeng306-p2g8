using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class PopulateEmployeeScroll : MonoBehaviour {

    // TODO: Average age, average employee rating, total spent, average spent, total employees
    public TextMeshProUGUI TotalLabel;
    public TextMeshProUGUI TotalSpentLabel;
    public TextMeshProUGUI AverageAgeLabel;
    public TextMeshProUGUI AverageRatingLabel;

    public GameObject ScrollViewContent;
    public GameObject EmployeeItemTemplate;

    public void Start()
    { 
        Reload();
    }

    public void Reload()
    {
        ClearElements(ScrollViewContent);

        // Populate statistics
        TotalLabel.text = "69";
        TotalSpentLabel.text = "$" + "4000";
        AverageAgeLabel.text = "69";
        AverageRatingLabel.text = "4.5";

        // For actually loopig through the game's npcs.
        foreach (KeyValuePair<GameObject, NPCInfo> NpcPair in NPCController.Instance.NpcInstances)
        {
            var copy = Instantiate(EmployeeItemTemplate);
            copy.transform.SetParent(ScrollViewContent.transform, false);

            var Element = copy.GetComponent<EmployeeScrollElement>();
            Element.Npc = NpcPair.Value;
            Element.Populate();
        }
    }

    private void ClearElements(GameObject scrollViewContent)
    {
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
