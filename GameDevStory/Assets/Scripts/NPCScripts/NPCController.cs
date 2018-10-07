using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// singleton that is responsible for all npcs that exist in the current scene
public class NPCController : Singleton<NPCController> {

    public GameObject npcTemplate; // the generic npc template to instantiate
	public CoordinateSystem coordinateSystem;
    public GameObject notificationButton;
    public GameObject[] bugButtons;

    private List<GameObject> npcInstances = new List<GameObject>(); // maintain a reference to each npc in the scene

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f; //todo adjust the scale of the world so we don't need to deal in tiny numbers

    // Use this for initialization
    void Start () {}

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowScenarioNotification(Scenario s)
    {

        GameObject npc = GetNPCWithoutNotification();
        if (npc == null)
            Debug.Log("No NPCs can accept a notification"); //TODO: add to a queue of notifications?

        // show the notification button in the scene
        GameObject buttonInstanceContainer = ShowButtonAboveNPC(npc, notificationButton);

        // access the button part of the notification component and register the scenario to be executed on click
        Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
        buttonInstance.onClick.AddListener(() =>
        {
            s.ExecuteScenario();
            npc.GetComponent<NPCBehaviour>().SetHasNotification(false);
            Debug.Log("Click!");
            Destroy(buttonInstanceContainer); // could set a delay as second param if desired
        });
              
    }

    // shows a random bug button and registers the "success" callback to be called when the button is pressed
    // if an npc is available to accept the bug. Otherwise the "failure" callback will be called straight away
    // so that the bug isn't counted as a missed bug
    public void ShowBug(Action success, Action failure)
    {
        GameObject npc = GetNPCWithoutNotification();
        if (npc == null)
        {
            failure();
            return;
        }       

        // select a random bug button and show it in the scene
        GameObject bugToShow = bugButtons[UnityEngine.Random.Range(0, bugButtons.Length)];
        GameObject buttonInstanceContainer = ShowButtonAboveNPC(npc, bugToShow);

        Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
        buttonInstance.onClick.AddListener(() =>
        {
            //TODO make this behaviour common to any button (bug or notification) by adding a separate listener on startup
            npc.GetComponent<NPCBehaviour>().SetHasNotification(false);
            Destroy(buttonInstanceContainer);

            success();
        });
    }

    public void AddNPCToScene(NPCInfo npc, Vector2 position)
    {
        InstantiateNPC(npc.attributes.animationController, position);
    }

    private GameObject InstantiateNPC(RuntimeAnimatorController animation, Vector3 pos)
    {
        //Vector3 pos = coordinateSystem.getVector3(position); // TODO: Finish coordinate system
        GameObject npcInstance = Instantiate(npcTemplate, pos, Quaternion.identity);
        npcInstance.GetComponent<Animator>().runtimeAnimatorController = animation; // set the animator controller
        npcInstance.transform.SetParent(this.transform); // npcs should show up as a child of the npc controller
        npcInstances.Add(npcInstance);
        return npcInstance;
    }

    // shows the specified button directly above the specified npc
    // returns the instantiated button so caller code may add
    // listeners etc.
    private GameObject ShowButtonAboveNPC(GameObject npc, GameObject button)
    {
        Vector3 npcCurrentPosition = npc.transform.position;
        GameObject instance = Instantiate(button, new Vector3(npcCurrentPosition.x, npcCurrentPosition.y + NOTIFICATION_HEIGHT_OFFSET, npcCurrentPosition.z), Quaternion.identity);
        instance.transform.SetParent(npc.transform);

        npc.GetComponent<NPCBehaviour>().SetHasNotification(true);

        return instance;
    }

    // finds one of the NPCs in the scene that is free to accept a notification
    // returns null if none are free
    // TODO: add randomness into the selection
    private GameObject GetNPCWithoutNotification()
    {
        foreach (GameObject npc in npcInstances)
        {
            NPCBehaviour npcScript = npc.GetComponent<NPCBehaviour>();
            if (!npcScript.GetHasNotification())
                return npc;
        }
        return null;
    }
}
