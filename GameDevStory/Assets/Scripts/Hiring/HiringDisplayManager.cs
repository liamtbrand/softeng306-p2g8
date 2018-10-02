using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringDisplayManager : MonoBehaviour {

    public Button HireButton;
    public GameObject GridViewPanel;
    public GameObject ApplicantViewPanel;

    void Start()
    {
        GridViewPanel.SetActive(false);
    }

    public void ShowHiringGrid()
    {
        HireButton.interactable = false;
        GridViewPanel.SetActive(true);
    }

    public void CloseHiringGrid()
    {
        HireButton.interactable = true;
        GridViewPanel.SetActive(false);
    }

    // The reason display applicant is not found in this script is because it is
    // located in TileManager.cs under Clicked(). This is because switching to an
    // applicant's view requires more information which is provided in the script.

    public void CloseApplicant()
    {
        ApplicantViewPanel.SetActive(false);
        GridViewPanel.SetActive(true); // Return to the grid view.
    }
}
