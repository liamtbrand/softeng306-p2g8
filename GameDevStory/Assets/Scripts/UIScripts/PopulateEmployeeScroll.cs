using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class PopulateEmployeeScroll : MonoBehaviour {

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
        // Clear scroll view for reload
        ClearElements(ScrollViewContent);

        int totalEmployees = 0;
        int totalSpent = 0;
        int totalAge = 0;
        float totalRating = 0;

        // Loop through all currently hired npcs
        foreach (KeyValuePair<GameObject, NPCInfo> NpcPair in NPCController.Instance.NpcInstances)
        {
            // Add a new item to the scroll view
            var copy = Instantiate(EmployeeItemTemplate);
            copy.transform.SetParent(ScrollViewContent.transform, false);

            var Element = copy.GetComponent<EmployeeScrollElement>();
            Element.Npc = NpcPair.Value;
            Element.Populate();

            // Record some stats
            totalEmployees++;
            totalSpent += NpcPair.Value.Attributes.ammountPaidFor;
            totalAge += NpcPair.Value.Attributes.age;

            // Rating takes values from all stats for a total out of 500.
            var totalForEmployee = NpcPair.Value.Stats.Communication + 
                NpcPair.Value.Stats.Creativity + 
                NpcPair.Value.Stats.Design + 
                NpcPair.Value.Stats.Technical + 
                NpcPair.Value.Stats.Testing;

            totalRating += totalForEmployee / 100f; // Normalize to out of 5.

        }

        // Populate statistics
        TotalLabel.text = totalEmployees.ToString();
        TotalSpentLabel.text = "$" + totalSpent.ToString();
        AverageAgeLabel.text = (totalAge / (float) totalEmployees).ToString("n2");
        AverageRatingLabel.text = (totalRating / totalEmployees).ToString("n2");
    }

    /**
     * Removes the current set of employee elements in the scroll view.
     */
    private void ClearElements(GameObject scrollViewContent)
    {
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
