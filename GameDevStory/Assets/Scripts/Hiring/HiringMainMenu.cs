using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringMainMenu : MonoBehaviour {

    public GameObject HiringMenuWindow;

	// Use this for initialization
	void Start () {
        HiringMenuWindow.SetActive(false);
	}
	
	void ShowHiringMenu()
    {
        HiringMenuWindow.SetActive(true);
    }
}
