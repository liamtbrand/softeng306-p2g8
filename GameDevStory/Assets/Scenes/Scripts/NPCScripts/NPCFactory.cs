using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour {

    public GameObject npcTemplate;

    public NPCAttributes[] npcs;

    public NPCAttributes SelectRandomNPCAttributes()
    {
        return npcs[Random.Range(0,npcs.Length)];
    }

    public NPCStats GetRandomStats()
    {
        NPCStats stats = new NPCStats();

        stats.communication = Random.Range(1, 101);
        stats.creativity = Random.Range(1, 101);
        stats.testing = Random.Range(1, 101);
        stats.technical = Random.Range(1, 101);
        stats.design = Random.Range(1, 101);

        return stats;
    }

    public NPC CreateRandomizedNPC()
    {
        NPC npc = new NPC();

        npc.attributes = SelectRandomNPCAttributes();
        npc.stats = GetRandomStats();

        return npc;
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
