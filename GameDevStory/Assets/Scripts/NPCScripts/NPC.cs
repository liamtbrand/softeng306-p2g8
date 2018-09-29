using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
public class NPC : MonoBehaviour
{
=======
// script containing state for an individual NPC
public class NPC : MonoBehaviour {
>>>>>>> Framework in place for sending an NPC scenario notifications which, on click, will execute the scenario

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f; //todo adjust the scale of the world so we don't need to deal in tiny numbers
    private bool hasNotification = false; // so we know when this NPC is available to show a notification

<<<<<<< HEAD
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            Instantiate(npcs[i], new Vector3(i * 0.2f, 0, 0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
=======
    public GameObject notificationButton;

    void Start()
    {
        Debug.Log("NPC Script initializing");
        //todo some stuff will surely go here when this gets more complex
    }


    public bool HasNotification()
    {
        return this.hasNotification;
    }

    public void ShowScenarioNotification(Scenario s)
    {
        Debug.Log("Showing Scenario Notification!");
        this.hasNotification = true;

        // show the notification button in the scene
        Vector3 npcCurrentPosition = transform.position;
        GameObject buttonInstanceContainer = Instantiate(notificationButton, new Vector3(npcCurrentPosition.x, npcCurrentPosition.y + NOTIFICATION_HEIGHT_OFFSET, 0f), Quaternion.identity);
        buttonInstanceContainer.transform.SetParent(this.transform);

        // access the button part of the notification component and register the scenario to be executed on click
        Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
        buttonInstance.onClick.AddListener(() => {
            s.ExecuteScenario();
            this.hasNotification = false;
            Debug.Log("Click!");
            Destroy(buttonInstanceContainer); // could set a delay as second param if desired
        });
    }

}
>>>>>>> Framework in place for sending an NPC scenario notifications which, on click, will execute the scenario
