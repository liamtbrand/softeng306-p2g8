using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton that is responsible for all npcs that exist in the current scene
public class NPCController : Singleton<NPCController> {

    public GameObject npcTemplate; // the generic npc template to instantiate
	public CoordinateSystem coordinateSystem;

    private List<GameObject> npcInstances = new List<GameObject>(); // maintain a reference to each npc in the scene

	// Use this for initialization
	void Start () {}

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowScenarioNotification(Scenario s)
    {
        
        foreach(GameObject npc in npcInstances)
        {
            NPCBehaviour npcScript = npc.GetComponent<NPCBehaviour>();
            if (!npcScript.HasNotification())
            {
                npcScript.ShowScenarioNotification(s);
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
}
