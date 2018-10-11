using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringDisplayManager : MonoBehaviour {

    public Button HireButton;
    public GameObject GridViewPanel;
    public GameObject ApplicantViewPanel;
    public GameObject NegotiatingPanel;


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
        ApplicantViewPanel.SetActive(false);
        NegotiatingPanel.SetActive(false);
        GridViewPanel.SetActive(true);
        ProjectManager.Instance.PauseProject();
    }

    public void CloseHiringGrid()
    {
        Debug.Log(LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().DeskAvailable());
        if (LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().DeskAvailable()) {
            EnableHireButton();
        }
        GridViewPanel.SetActive(false);
        ProjectManager.Instance.ResumeProject();
    }

    public void ShowNegotiating()
    {
        ApplicantViewPanel.SetActive(false);
        NegotiatingPanel.SetActive(true);
    }

    public void HideNegotiating()
    {
        NegotiatingPanel.SetActive(false);
        ApplicantViewPanel.SetActive(true);
    }

    // The reason display applicant is not found in this script is because it is
    // located in TileManager.cs under Clicked(). This is because switching to an
    // applicant's view requires more information which is provided in the script.

    public void CloseApplicant()
    {
        ApplicantViewPanel.SetActive(false);
        ProjectManager.Instance.ResumeProject();
    }
}
