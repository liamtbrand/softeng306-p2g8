using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// singleton that is responsible for all npcs that exist in the game
public class NPCController : Singleton<NPCController> {

    public GameObject npcTemplate; // the generic npc template to instantiate
    public GameObject notificationButton;
    public GameObject[] bugButtons;

    private readonly Dictionary<GameObject, NPCInfo> _npcInstances = new Dictionary<GameObject, NPCInfo>(); // maintain a reference to each npc in the scene, along with their info

    public Dictionary<GameObject, NPCInfo> NpcInstances
    {
        get { return _npcInstances; }
    }

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f; //todo adjust the scale of the world so we don't need to deal in tiny numbers

    private static int npcsToAdd = 2;										// TODO: Don't hardcode this

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < npcsToAdd; i++)
        {
            HireEmployee(NPCFactory.Instance.CreateNPCWithRandomizedStats());
        }
    }

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowNotification(Action a, GameObject npc)
    {

        if (npc == null)
            Debug.Log("No NPCs can accept a notification"); //TODO: add to a queue of notifications?

        // show the notification button in the scene
        GameObject buttonInstanceContainer = ShowButtonAboveNPC(npc, notificationButton);

        // access the button part of the notification component and register the scenario to be executed on click
        Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
        buttonInstance.onClick.AddListener(() =>
        {
            a.Invoke();
            npc.GetComponent<NPCBehaviour>().SetHasNotification(false);
            Debug.Log("Click!");
            Destroy(buttonInstanceContainer); // could set a delay as second param if desired
        });
    }
    
    // Overload to use Random NPC with Scenario
    public void ShowScenarioNotification(Scenario s)
    {
        ShowNotification(s.ExecuteScenario, GetNpcWithoutNotification());
    }

    // shows a random bug button and registers the "success" callback to be called when the button is pressed
    // if an npc is available to accept the bug. Otherwise the "failure" callback will be called straight away
    // so that the bug isn't counted as a missed bug
    public void ShowBug(Action success, Action failure)
    {
        GameObject npc = GetNpcWithoutNotification();
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
        InstantiateNPC(npc.Attributes.animationController, position, npc);
    }

    // Method to be called to hire an employee.
    // This will take care of randomly placing the NPC into the level.
    public void HireEmployee(NPCInfo npcInfo)
    {
        Vector2 position = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().GetRandomFreeDeskPosition();
        AddNPCToScene(npcInfo, LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().GetDeskNPCPosition(position));
    }

    private GameObject InstantiateNPC(RuntimeAnimatorController animation, Vector2 position, NPCInfo info)
    {
        Vector3 pos = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().coordinateSystem.getVector3(position); // TODO: Finish coordinate system
        GameObject npcInstance = Instantiate(npcTemplate, pos, Quaternion.identity);
        npcInstance.GetComponent<Animator>().runtimeAnimatorController = animation; // set the animator controller
        npcInstance.transform.SetParent(this.transform); // npcs should show up as a child of the npc controller
        _npcInstances.Add(npcInstance, info);
        return npcInstance;
    }

    // shows the specified button directly above the specified npc
    // returns the instantiated button so caller code may add
    // listeners etc.
    private GameObject ShowButtonAboveNPC(GameObject npc, GameObject button)
    {
        Vector3 npcCurrentPosition = npc.transform.position;
        Vector3 pos = new Vector3(0, 0, 0);
        pos.x = npcCurrentPosition.x;                                 // Default x position of npc
        pos.y = npcCurrentPosition.y + NOTIFICATION_HEIGHT_OFFSET;    // Add offset to shift notification up on screen
        pos.z = npcCurrentPosition.z + 1;                             // Add 1 to bring the popup forwards
        GameObject instance = Instantiate(button, pos, Quaternion.identity);
        instance.transform.SetParent(npc.transform);

        npc.GetComponent<NPCBehaviour>().SetHasNotification(true);

        return instance;
    }

    // finds one of the NPCs in the scene that is free to accept a notification
    // returns null if none are free
    // TODO: add randomness into the selection
    private GameObject GetNpcWithoutNotification()
    {
        foreach (GameObject npc in _npcInstances.Keys)
        {
            NPCBehaviour npcScript = npc.GetComponent<NPCBehaviour>();
            if (!npcScript.GetHasNotification())
                return npc;
        }
        return null;
    }
}
