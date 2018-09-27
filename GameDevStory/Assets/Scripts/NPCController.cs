using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public GameObject[] npcs;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < npcs.Length; i++)
        {
            Instantiate(npcs[i], new Vector3(i * 0.2f, 0, 0f), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
