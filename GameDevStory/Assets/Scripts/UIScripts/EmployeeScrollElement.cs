using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeScrollElement : MonoBehaviour {

    // Game object representing a form of the "applicant view" only now it shows an employee.
    public GameObject EmployeeView;

    public Image HeadShot;
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI AgeLabel;
    public TextMeshProUGUI GenderLabel;
    public TextMeshProUGUI PaidLabel;

    public NPCInfo Npc { get; set; }

    /**
     * Populate value of a list item from an npc object
     */
    public void Populate()
    {
        HeadShot.sprite = Npc.Attributes.headshot;
        NameLabel.text = Npc.Attributes.npcName;
        AgeLabel.text = Npc.Attributes.age.ToString();
        GenderLabel.text = Npc.Attributes.gender.ToString();
        PaidLabel.text = Npc.Attributes.ammountPaidFor != 0 
            ? "$" + Npc.Attributes.ammountPaidFor
            : "N/A";
    }

    public void clicked()
    {
        EmployeeView.GetComponent<InitialiseEmployeeView>().npcInfo = Npc;
        EmployeeView.GetComponent<InitialiseEmployeeView>().Reload();
    }
}
