using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeScrollElement : MonoBehaviour {

    public Image HeadShot;
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI AgeLabel;
    public TextMeshProUGUI GenderLabel;
    public TextMeshProUGUI PaidLabel;

    public NPCInfo Npc { get; set; }

    public void Populate()
    {
        HeadShot.sprite = Npc.Attributes.headshot;
        NameLabel.text = Npc.Attributes.npcName;
        AgeLabel.text = Npc.Attributes.age.ToString();
        GenderLabel.text = Npc.Attributes.gender.ToString();
        // TODO: FIll in aid for element
    }
}
