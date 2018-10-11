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

    public void Refresh() {
        // Get the npc's stat from their stats script
        npcInfo = NPCFactory.Instance.CreateNPCWithRandomizedStats();
        image.GetComponent<Animator>().runtimeAnimatorController = npcInfo.Attributes.animationController;

        nameHeader.text = npcInfo.Attributes.npcName;
        ageHeader.text = npcInfo.Attributes.age.ToString();
        genderHeader.text = npcInfo.Attributes.gender.ToString();
        costHeader.text = "$" + npcInfo.Attributes.cost.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.getBalance() < npcInfo.Attributes.cost)
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
