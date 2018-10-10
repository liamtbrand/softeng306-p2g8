using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class JobAdManager : MonoBehaviour {

    // The job advertisement screen to be shown
    public GameObject jobAdEditor;

    void Start() {
        jobAdEditor.SetActive(false);
    }

    // Display the job advertisement editor
    public void ShowEditor()
    {
        jobAdEditor.SetActive(true);
    }

    // This should be called to start the first level
    public void CloseEditor()
    {
        jobAdEditor.SetActive(false);
    }

}