using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDisplayTestScript : MonoBehaviour
{
    public ProjectDisplayManager DisplayManager;
    
    public void TestAddProject()
    {
        DisplayManager.AddNewProject("Lorem Ipsum",
            "Rashina, Inc",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            "Really expensive\nYeah really", true, delegate { print("Callback for normal project"); });
    }
    
    public void TestAddDisabledProject()
    {
        DisplayManager.AddNewProject("Lorem Ipsum",
            "Rashina, Inc",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            "Really expensive\nYeah really", false);
    }

}
