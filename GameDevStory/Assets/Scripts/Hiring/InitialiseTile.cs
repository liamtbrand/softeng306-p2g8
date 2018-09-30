using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialiseTile : MonoBehaviour {

    // NPC to be displayed
    public GameObject npc;

    // Misc. character info
    public Image spriteImage;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;

    // Use this for initialization
    void Start()
    {
        // Get the npc's stat from their stats script
        var stats = npc.GetComponent<CharacterStats>();

        spriteImage.sprite = stats.sprite;
        nameHeader.text = stats.name;
        ageHeader.text = stats.age.ToString();
        genderHeader.text = stats.gender.ToString();
    }

    void Update()
    {
        Start();
    }

}
