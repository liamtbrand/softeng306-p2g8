using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : Singleton<NPCController> { 

    // for npcs, but where the desk are.
	public static Vector2[] npcPositions = {
        new Vector2(0,0),
        new Vector2(0,4),
        new Vector2(2.5f,1),
		new Vector2(2.5f,3),
        new Vector2(1.5f,1)
    };

    public GameObject npcTemplate; // the generic npc template to instantiate
    public RuntimeAnimatorController[] animatorControllers; // pool of possible animations to give the generated npc

	public CoordinateSystem coordinateSystem;

	private GameObject InstantiateNPC(RuntimeAnimatorController animation, Vector2 position)
	{
		Vector3 pos = coordinateSystem.getVector3(position);
		GameObject npcInstance = Instantiate(npcTemplate, pos, Quaternion.identity);
		npcInstance.GetComponent<Animator>().runtimeAnimatorController = animation; // set the animator controller
        npcInstance.transform.SetParent(this.transform); // npcs should show up as a child of the npc controller
		npcInstances.Add(npcInstance);
		return npcInstance;
	}

    private List<GameObject> npcInstances = new List<GameObject>(); // maintain a reference to each npc in the scene

	// Use this for initialization
	void Start () {

		for (int i = 0; i < npcPositions.Length; i++)
        {
			InstantiateNPC(animatorControllers[i % animatorControllers.Length], npcPositions[i]);
        }
    }

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowScenarioNotification(Scenario s)
    {
		/*
        foreach(GameObject npc in npcInstances)
        {
            NPC npcScript = npc.GetComponent<NPC>();
            if (!npcScript.HasNotification())
            {
                npcScript.ShowScenarioNotification(s);
            }
            else
            {
                // could add scenario to a queue so it shows when it is free, depends on what we decide
                Debug.Log("No NPC free to accept notification");
            }
        }
        */
    }
}
