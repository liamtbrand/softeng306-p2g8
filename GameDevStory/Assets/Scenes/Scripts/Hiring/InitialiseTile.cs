using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialiseTile : MonoBehaviour {

    // Misc. character info
    public GameObject image;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;

    // Use this for initialization
    void Start()
    {
        // TODO: Finish implementing
        // Get the npc's stat from their stats script
        var npc = NPCFactory.Instance.CreateNPCWithRandomizedStats();
        image.GetComponent<Animator>().runtimeAnimatorController = npc.attributes.animationController;

        //spriteImage.sprite = npc.attributes.sprite;
        nameHeader.text = npc.attributes.name;
        ageHeader.text = npc.attributes.age.ToString();
        genderHeader.text = npc.attributes.gender.ToString();
    }

    void Update()
    {
    }

}
