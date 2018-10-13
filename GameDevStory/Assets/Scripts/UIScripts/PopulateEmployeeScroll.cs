using UnityEngine;
using System.Collections.Generic;
using TMPro;

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
        // Populate statistics
        TotalLabel.text = "69";
        TotalSpentLabel.text = "$" + "4000";
        AverageAgeLabel.text = "69";
        AverageRatingLabel.text = "4.5";

        // For actually loopig through the game's npcs.
        foreach (KeyValuePair<GameObject, NPCInfo> NpcPair in NPCController.Instance.NpcInstances)
        {
            var copy = Instantiate(EmployeeItemTemplate);
            //copy.GetComponent<TextMeshProUGUI>().text = NpcPair.Value.Attributes.npcName;
            copy.transform.SetParent(ScrollViewContent.transform, false);

            var Element = copy.GetComponent<EmployeeScrollElement>();
            Element.Npc = NpcPair.Value;
            Element.Populate();
        }




        //var name = "a";
        //var cost = 0;
        //var age = 50;
        //for (int i = 0; i < 10; i++)
        //{
        //    var copy = Instantiate(EmployeeItemTemplate);
        //    copy.transform.SetParent(ScrollViewContent.transform, false);

        //    var npc = new NPCInfo();
        //    npc.Attributes = new NPCAttributes();
        //    npc.Attributes.npcName = name;
        //    npc.Attributes.gender = NPCAttributes.Gender.FEMALE;
        //    npc.Attributes.cost = cost;
        //    npc.Attributes.age = age;

        //    var Element = copy.GetComponent<EmployeeScrollElement>();
        //    Element.Npc = npc;
        //    Element.Populate();

        //    name += "a";
        //    cost++;
        //    age++;
        //}
    }

}
