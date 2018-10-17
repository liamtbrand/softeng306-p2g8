using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringDisplayManager : MonoBehaviour {

    public Button HireButton;
    public GameObject HiringMainMenu;
    public GameObject GridViewPanel;
    public GameObject ApplicantViewPanel;
    public GameObject NegotiatingPanel;
    public GameObject CurrentEmployeesPanel;
    public GameObject EmployeePanel;
    private bool fromProjectMenu;


    void Start()
    {
        HiringMainMenu.SetActive(false);
        GridViewPanel.SetActive(false);
        ApplicantViewPanel.SetActive(false);
        NegotiatingPanel.SetActive(false);
    }

    public void DisableHireButton() {
        HireButton.interactable = false;
    }

    public void EnableHireButton() {
        HireButton.interactable = true;
    }

    public void ShowHiringMenu()
    {
        // whenever hiring menu is shown, check if we have space for another employee
        if (LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().DeskAvailable())
            EnableHireButton();
        else
            DisableHireButton();
        HiringMainMenu.SetActive(true);
        if (ProjectManager.Instance.projectMenu.activeSelf) {
            ProjectManager.Instance.HideProjectMenu();
            fromProjectMenu = true;
        } else {
            ProjectManager.Instance.PauseProject(); 
        }
    }

    public void HideHiringMenu()
    {
        HiringMainMenu.SetActive(false);
    }

    public void HideHiringMenuAndResumeProject()
    {
        HideHiringMenu();
        if (fromProjectMenu) {
            fromProjectMenu = false;
            ProjectManager.Instance.PickProject();
        } else {
            ProjectManager.Instance.ResumeProject();    
        }
    }

    public void ShowHiringGrid()
    {
        HideHiringMenu();
        ApplicantViewPanel.SetActive(false);
        NegotiatingPanel.SetActive(false);
        GridViewPanel.SetActive(true);
    }

    public void CloseHiringGrid()
    {
        GridViewPanel.SetActive(false);
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

    public void ShowCurrentEmployees()
    {
        CurrentEmployeesPanel.GetComponent<PopulateEmployeeScroll>().Reload();
        HiringMainMenu.SetActive(false);
        CurrentEmployeesPanel.SetActive(true);
    }

    public void HideCurrentEmployees()
    {
        CurrentEmployeesPanel.SetActive(false);
        HiringMainMenu.SetActive(true);
    }

    // The reason display applicant is not found in this script is because it is
    // located in TileManager.cs under Clicked(). This is because switching to an
    // applicant's view requires more information which is provided in the script.

    public void CloseApplicant()
    {
        ApplicantViewPanel.SetActive(false);
    }

    public void ShowEmployee()
    {
        CurrentEmployeesPanel.SetActive(false);
        EmployeePanel.SetActive(true);
    }

    public void HideEmployee()
    {
        EmployeePanel.SetActive(false);
        CurrentEmployeesPanel.SetActive(true);
    }
}
