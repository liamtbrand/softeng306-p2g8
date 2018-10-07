using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton that is responsible for all npcs that exist in the current scene
public class NPCController : Singleton<NPCController> {

    public GameObject npcTemplate; // the generic npc template to instantiate
	public CoordinateSystem coordinateSystem;

    private readonly Dictionary<GameObject, NPCInfo> _npcInstances = new Dictionary<GameObject, NPCInfo>(); // maintain a reference to each npc in the scene, along with their info

    public Dictionary<GameObject, NPCInfo> NpcInstances
    {
        get { return _npcInstances; }
    }

    // Use this for initialization
	void Start () {}

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowScenarioNotification(Scenario s)
    {
        
        foreach(var npc in _npcInstances.Keys)
        {
            var npcScript = npc.GetComponent<NPCBehaviour>();
            if (!npcScript.HasNotification())
            {
                npcScript.ShowGenericNotification(s.ExecuteScenario);
                return;
            }
            else
            {
                // could add scenario to a queue so it shows when it is free, depends on what we decide
                Debug.Log("No NPC free to accept notification");
            }
        }
              
    }

    public void AddNPCToScene(NPCInfo npc, Vector2 position)
    {
        InstantiateNPC(npc.Attributes.animationController, position, npc);
    }

    private GameObject InstantiateNPC(RuntimeAnimatorController animation, Vector3 pos, NPCInfo info)
    {
        //Vector3 pos = coordinateSystem.getVector3(position); // TODO: Finish coordinate system
        GameObject npcInstance = Instantiate(npcTemplate, pos, Quaternion.identity);
        npcInstance.GetComponent<Animator>().runtimeAnimatorController = animation; // set the animator controller
        npcInstance.transform.SetParent(this.transform); // npcs should show up as a child of the npc controller
        _npcInstances.Add(npcInstance, info);
        return npcInstance;
    }
}
