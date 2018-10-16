using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// singleton that is responsible for all npcs that exist in the game
public class NPCController : Singleton<NPCController> {

    public GameObject npcTemplate; // the generic npc template to instantiate
    public GameObject scenarioButton;
    public GameObject[] bugButtons;

    private readonly Dictionary<GameObject, NPCInfo> _npcInstances = new Dictionary<GameObject, NPCInfo>(); // maintain a reference to each npc in the scene, along with their info

    public Dictionary<GameObject, NPCInfo> NpcInstances
    {
        get { return _npcInstances; }
    }

    // stores a mapping from npc game object to the desk that npc is occupying.
    private Dictionary<GameObject, Vector2> acquiredDesks = new Dictionary<GameObject, Vector2>();

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f;

    private static int npcsToAdd = 2;										// TODO: Don't hardcode this
    private bool dialogueIsActive = false; // TODO

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < npcsToAdd; i++)
        {
            HireEmployee(NPCFactory.Instance.CreateNPCWithRandomizedStats());
        }
    }

    // shows a scenario notification above a specific npc
    public void ShowScenarioNotification(Action a, GameObject npc)
    {
        Button button = ShowNotification(a, npc, scenarioButton).GetComponentInChildren<Button>();
    }

    // Overload to use Random NPC with Scenario
    public void ShowScenarioNotification(Scenario s)
    {
        GameObject npc = GetNpcWithoutNotification();
        Button button = ShowNotification(s.ExecuteScenario, npc, scenarioButton).GetComponentInChildren<Button>();
    }

    public void RemoveNPC(GameObject npc)
    {
        _npcInstances.Remove(npc);
        LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().FreeDesk(acquiredDesks[npc]);
        acquiredDesks.Remove(npc);
        Destroy(npc);
    }

    // shows a random bug button and registers the provided action to be called when the bug is pressed
    // or when the bug cannot be shown and should still be counted as squashed to avoid incorrect
    // profit calculations.
    public void ShowBug(Action onBugSquashed)
    {
        GameObject npc = GetNpcWithoutNotification();
        if (npc == null)
        {
            onBugSquashed.Invoke();     // no npcs available but bug was not missed so still count as squashed
            return;
        }

        // select a random bug button and show it in the scene
        GameObject bugToShow = bugButtons[UnityEngine.Random.Range(0, bugButtons.Length)];

        GameObject buttonContainer = ShowNotification(onBugSquashed, npc, bugToShow);

        // register bug to disappear after two seconds if not clicked. If project is paused (i.e. a menu is shown over the scene),
        // these destroyed bugs won't be counted as missed
        StartCoroutine(TearDownButtonAfterDelay(npc.GetComponent<NPCBehaviour>(), buttonContainer, 2, onBugSquashed));
    }


    public void AddNPCToScene(NPCInfo npc)
    {
        // Place the npc on screen
        Vector2 position = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().GetRandomFreeDeskPosition();
        LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().AcquireDesk(position);
        GameObject npcGO = InstantiateNPC(
            npc.Attributes.animationController,
            LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().GetDeskNPCPosition(position),
            npc
        );
        acquiredDesks.Add(npcGO,position);
    }

    // Method to be called to hire an employee.
    // Notifies the npcfactory we have hired the npc.
    // This will also take care of randomly placing the NPC into the level.
    public void HireEmployee(NPCInfo npcInfo)
    {
        // Need to notify the npcfactory so we can't reproduce this npc.
        NPCFactory.Instance.RemoveNPCFromPool(npcInfo);
        AddNPCToScene(npcInfo);
    }

    public List<NPCInfo> TeardownNpcs(){

        var npcList = new List<NPCInfo>();
        var destroyList = new List<KeyValuePair<GameObject,NPCInfo>>();
        // Destroying npcs and returning their info for rehiring/reinstantion on new level
        Debug.Log("Destroying NPCS");
        foreach(KeyValuePair<GameObject, NPCInfo> npcPair in _npcInstances){
            destroyList.Add(npcPair); // destroy list used to avoid invalid operation exceptions
            npcList.Add(npcPair.Value);

        }

        foreach(var pair in destroyList){
            _npcInstances.Remove(pair.Key);
            Destroy(pair.Key);
        }

        return npcList;
    }

    private GameObject InstantiateNPC(RuntimeAnimatorController animation, Vector2 position, NPCInfo info)
    {
        Vector3 pos = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().coordinateSystem.getVector3(position);
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
        // get all npcs who are free to accept a notification
        var npcsWithoutNotification = new List<GameObject>();
        foreach (GameObject npc in _npcInstances.Keys)
        {
            NPCBehaviour npcScript = npc.GetComponent<NPCBehaviour>();
            if (!npcScript.GetHasNotification())
                npcsWithoutNotification.Add(npc);
        }

        if (npcsWithoutNotification.Count == 0)
            return null;    // no npc is available to accept notification

        // return random npc who has no notification
        return npcsWithoutNotification[UnityEngine.Random.Range(0, npcsWithoutNotification.Count)];
    }

    // Sends a notification to an npc that the player should click on. On click,
    // the notification will be hidden and the provided action will be invoked.
    // The instantiated button is returned in case caller code wants to modify behaviour
    private GameObject ShowNotification(Action a, GameObject npc, GameObject button)
    {

        if (npc == null)
            Debug.Log("Could not show notification, npc not specified");

        // show the notification button in the scene
        GameObject buttonInstanceContainer = ShowButtonAboveNPC(npc, button);

        // access the button part of the notification component and register the action to be executed on click
        Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
        buttonInstance.onClick.AddListener(() =>
        {
            a.Invoke();
            npc.GetComponent<NPCBehaviour>().SetHasNotification(false);
            Destroy(buttonInstanceContainer); // could set a delay as second param if desired
        });

        return buttonInstanceContainer;
    }

    // to be launched as a coroutine when we want a button to disappear after a certain time
    private IEnumerator TearDownButtonAfterDelay(NPCBehaviour npc, GameObject button, float delayInSeconds, Action isPausedCallback)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (ProjectManager.Instance.IsPaused())
            isPausedCallback.Invoke();

        npc.SetHasNotification(false);
        Destroy(button);
    }
}
