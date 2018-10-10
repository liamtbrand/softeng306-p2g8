using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A level contains the information for the background, and the office layout.
/// </summary>
public class Level : MonoBehaviour {

    public OfficeLayout officeLayout;
    public GameObject background;

    public BaseLevelSetupScript setupScript;

    // private Transform levelHolder;                                          //A variable to store a reference to the transform of our Board object.

    public OfficeLayout GetOfficeLayout()
    {
        return officeLayout;
    }

    // holds a reference to instantiated background so we can clean it up again
    private GameObject bg;

    public void SetupLevel()
    {
        // Instantiate the level background
        bg = Instantiate(background, new Vector3(0, 0, 1f), Quaternion.identity);

        if(setupScript != null){
            setupScript.setup();
        }

    }

    public void TearDownLevel()
    {
        // Remove the background for this level
        Destroy(bg);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
