using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PopulateEmployeeScroll : MonoBehaviour {

    public GameObject Content;
    public GameObject EmployeeItemTemplate;

    public void Start()
    {
 
        Reload();
    }

    public void Reload()
    {
        // For actually loopig through the game's npcs.
        //foreach(KeyValuePair<GameObject, NPCInfo> NpcPair in NPCController.Instance.NpcInstances)
        //{
        //    var copy = Instantiate(EmployeeItemTemplate);
        //    copy.GetComponent<TextMeshProUGUI>().text = NpcPair.Value.Attributes.npcName;
        //    copy.transform.parent = Content.transform;
        //}
        var name = "a";
        var cost = 0;
        var age = 50;
        for (int i = 0; i < 10; i++)
        {
            var copy = Instantiate(EmployeeItemTemplate);
            copy.transform.parent = Content.transform;

            var npc = new NPCInfo();
            npc.Attributes = new NPCAttributes();
            npc.Attributes.npcName = name;
            npc.Attributes.gender = NPCAttributes.Gender.FEMALE;
            npc.Attributes.cost = cost;
            npc.Attributes.age = age;

            var Element = copy.GetComponent<EmployeeScrollElement>();
            Element.Npc = npc;
            Element.Populate();

            name += "a";
            cost++;
            age++;
        }
    }

}
