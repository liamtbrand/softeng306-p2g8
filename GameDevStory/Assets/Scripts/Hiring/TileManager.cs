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
    public TextMeshProUGUI costHeader;
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
        costHeader.text = "$" + npcInfo.attributes.cost.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.getBalance() < npcInfo.attributes.cost)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Clicked()
    {
        // Set the npc info field in applicant view script to be this applicant.
        InitialiseApplicantView.npcInfo = npcInfo;
        InitialiseApplicantView.clickedTile = this.gameObject.GetComponent<Button>();

        ApplicantView.GetComponent<InitialiseApplicantView>().Reload();
        ApplicantView.SetActive(true);
        GridView.SetActive(false); // Hide the underlying grid view in case
    }
}