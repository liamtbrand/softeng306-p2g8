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
        foreach(KeyValuePair<GameObject, NPCInfo> NpcPair in NPCController.Instance.NpcInstances)
        {
            var copy = Instantiate(EmployeeItemTemplate);
            copy.GetComponent<TextMeshProUGUI>().text = NpcPair.Value.Attributes.npcName;
            copy.transform.parent = Content.transform;
        }
    }

}
