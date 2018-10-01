using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InitialiseTile : MonoBehaviour {

    // Misc. character info
    public GameObject image;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;

    private NPCInfo npcInfo;

    // Use this for initialization
    void Start()
    {
        // TODO: Finish implementing
        // Get the npc's stat from their stats script
        npcInfo = NPCFactory.Instance.CreateNPCWithRandomizedStats();
        image.GetComponent<Animator>().runtimeAnimatorController = npcInfo.attributes.animationController;

        //spriteImage.sprite = npc.attributes.sprite;
        nameHeader.text = npcInfo.attributes.npcName;
        ageHeader.text = npcInfo.attributes.age.ToString();
        genderHeader.text = npcInfo.attributes.gender.ToString();
    }

    public void Clicked()
    {
        // Set the npc info field in applicant view script to be this applicant.
        InitialiseApplicantView.npcInfo = npcInfo;
        GameManager.Instance.switchScene(GameScene.ApplicantView);
    }
}
