using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {

    // Misc. character info
    public GameObject image;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;
    public GameObject ApplicantView;
    public GameObject GridView;

    private NPCInfo npcInfo;

    // Use this for initialization
    void Start()
    {
        // Get the npc's stat from their stats script
        npcInfo = NPCFactory.Instance.CreateNPCWithRandomizedStats();
        image.GetComponent<Animator>().runtimeAnimatorController = npcInfo.attributes.animationController;

        nameHeader.text = npcInfo.attributes.npcName;
        ageHeader.text = npcInfo.attributes.age.ToString();
        genderHeader.text = npcInfo.attributes.gender.ToString();
    }

    public void Clicked()
    {
        // Set the npc info field in applicant view script to be this applicant.
        InitialiseApplicantView.npcInfo = npcInfo;
        ApplicantView.GetComponent<InitialiseApplicantView>().Reload();
        ApplicantView.SetActive(true);
        GridView.SetActive(false); // Hide the underlying grid view in case
    }
}
