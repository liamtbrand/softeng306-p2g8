using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringDisplayManager : MonoBehaviour {

    public GameObject GridViewPanel;
    public GameObject ApplicantViewPanel;

    public void ShowHiringGrid()
    {
        GridViewPanel.SetActive(true);
    }

    public void CloseHiringGrid()
    {
        GridViewPanel.SetActive(false);
    }
}
