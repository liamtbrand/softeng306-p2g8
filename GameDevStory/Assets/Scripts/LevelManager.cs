using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.


public class LevelManager : MonoBehaviour {

	private float xSpacing = 0.3f;									// Spacing along x axis.
	private float ySpacing = 0.15f;									// Spacing along y axis.
	private float NPCSpacing = 0.08f;								// Spacing between desk and NPC.

	private int desksPerRow = 2;									// Hard coded for now.

    private int npcsToAdd = 2;										// TODO: Don't hardcode this

    public GameObject[] levels;                                 	//Array of level prefabs.
	public GameObject[] desks;                                 		//Array of desk prefabs.

	private Transform levelHolder;                                  //A variable to store a reference to the transform of our Board object.
    private static List <Vector3> NPCPositions = new List <Vector3> ();    //A list of possible locations to place NPCs.
    
    //Sets up the desks of the game level.
    private void LevelSetup ()
    {
		Debug.Log("Level should set up");
        //Instantiate Level and set levelHolder to its transform.
        levelHolder = new GameObject ("Level").transform;

		// Set up front row of desks
		float xFrontPos = -0.3f;
		float yFrontPos = -0.3f;
		for (int x = 0; x < desksPerRow; x++) {
			GameObject toInstantiate = desks[0];
			
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject instance = 
				Instantiate (toInstantiate, new Vector3(xFrontPos,yFrontPos,0f), Quaternion.identity) as GameObject;
			
			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent (levelHolder);

			NPCPositions.Add (new Vector3(xFrontPos+NPCSpacing, yFrontPos+NPCSpacing, 0f));
			xFrontPos+=xSpacing;
			yFrontPos-=ySpacing;
		}

		// Set up back row of desks
		float xBackPos = 0f;
		float yBackPos = -0.15f;
		for (int x = 0; x < desksPerRow; x++) {
			GameObject toInstantiate = desks[0];
			
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject instance = 
				Instantiate (toInstantiate, new Vector3(xBackPos,yBackPos,0f), Quaternion.identity) as GameObject;
			
			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent (levelHolder);

			NPCPositions.Add (new Vector3(xBackPos+NPCSpacing, yBackPos+NPCSpacing, 0f));
			xBackPos+=xSpacing;
			yBackPos-=ySpacing;
		}
    }

    public static bool DeskAvailable() {
        if (NPCPositions.Count == 0) {
            return false;
        } else {
            return true;
        }
    }

    public void AddEmployeeToLevel(NPCInfo npcInfo) {
        Vector3 position = GetRandomPosition();
        NPCController.Instance.AddNPCToScene(npcInfo, position);
        // Desk is now taken
        NPCPositions.Remove(position);
    }

	// Sets up the NPC at the desk
	private void NPCSetup ()
	{
        for (int i = 0; i < npcsToAdd; i++) {
            AddEmployeeToLevel(NPCFactory.Instance.CreateNPCWithRandomizedStats());
        }
	}

    private Vector3 GetRandomPosition() {
        if (NPCPositions.Count == 0)
        {
            // TODO disable buttons etc.
            throw new Exception("Cannot hire employee - not enough desks.");
        }
        else
        {
            return NPCPositions[Random.Range(0, NPCPositions.Count - 1)];
        }
    }
    
    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene (int level)
    {
		// Instantiate the level background
		Instantiate (levels[level], new Vector3 (0, 0, 0f), Quaternion.identity);

        //Creates the outer walls and floor.
        LevelSetup ();
		NPCSetup ();
    }
}
