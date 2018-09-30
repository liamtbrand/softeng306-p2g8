using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
<<<<<<< HEAD
public class NPCController : MonoBehaviour
{
=======
public class NPCController : Singleton<NPCController> { 
>>>>>>> Framework in place for sending an NPC scenario notifications which, on click, will execute the scenario

    public static float[][] deskPositions = {
        new float[]{0.06f,-0.06f},
        new float[]{0.08f,-0.37f},
        new float[]{-0.24f,-0.21f}
    };

    public GameObject[] npcs;

<<<<<<< HEAD
    // Use this for initialization
    void Start()
    {
        float x;
        float y;
        for (int i = 0; i < npcs.Length; i++)
        {
            x = deskPositions[i % 3][0];
            y = deskPositions[i % 3][1];
            Instantiate(npcs[i], new Vector3(x, y, 0f), Quaternion.identity);
=======
=======
public class NPCController : Singleton<NPCController> { 

	public static float[][] deskPositions = {
		new float[]{0.06f,-0.06f},
		new float[]{0.08f,-0.37f},
		new float[]{-0.24f,-0.21f}
	};

    public GameObject[] npcs;

>>>>>>> 69db4beab69610d790c57e5212046aed3fd8deb8
    private List<GameObject> npcInstances = new List<GameObject>(); // maintain a reference to each npc in the scene

	// Use this for initialization
	void Start () {
		float x;
		float y;
		for (int i = 0; i < npcs.Length; i++)
        {
			x = deskPositions[i % 3][0];
			y = deskPositions[i % 3][1];
            GameObject npcInstance = Instantiate(npcs[i], new Vector3(x, y, 0f), Quaternion.identity);
            npcInstance.transform.SetParent(this.transform); // npcs should show up as a child of the npc controller
            npcInstances.Add(npcInstance);
<<<<<<< HEAD
>>>>>>> 
        }
    }

<<<<<<< HEAD
    // Update is called once per frame
    void Update()
=======
    // Sends a scenario notification to an npc that the player should click on to start the scenario.
<<<<<<< HEAD
    public void PopUpScenario(Scenario s)
>>>>>>> Framework in place for sending an NPC scenario notifications which, on click, will execute the scenario
=======
    public void ShowScenarioNotification(Scenario s)
>>>>>>> 
=======
        }
    }

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void ShowScenarioNotification(Scenario s)
>>>>>>> 69db4beab69610d790c57e5212046aed3fd8deb8
    {
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
    }
}
<<<<<<< HEAD

=======
>>>>>>> 69db4beab69610d790c57e5212046aed3fd8deb8
