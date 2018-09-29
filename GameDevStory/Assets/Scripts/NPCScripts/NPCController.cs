using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Singleton<NPCController> { 

	public static float[][] deskPositions = {
		new float[]{0.06f,-0.06f},
		new float[]{0.08f,-0.37f},
		new float[]{-0.24f,-0.21f}
	};

    public GameObject[] npcs;

	// Use this for initialization
	void Start () {
		float x;
		float y;
		for (int i = 0; i < npcs.Length; i++)
        {
			x = deskPositions[i % 3][0];
			y = deskPositions[i % 3][1];
            Instantiate(npcs[i], new Vector3(x, y, 0f), Quaternion.identity);
        }
    }

    // Sends a scenario notification to an npc that the player should click on to start the scenario.
    public void PopUpScenario(Scenario s)
    {

    }
}
