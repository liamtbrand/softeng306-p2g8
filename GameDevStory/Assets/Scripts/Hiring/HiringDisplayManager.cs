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

    public void DisableHireButton() {
        HireButton.interactable = false;
    }

    public void EnableHireButton() {
        HireButton.interactable = true;
    }

    public void ShowHiringGrid()
    {
        DisableHireButton();
        GridViewPanel.SetActive(true);
    }

    public void CloseHiringGrid()
    {
        if (LevelManager.DeskAvailable()) {
            EnableHireButton();
        }
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
