using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

/// This class can be used to setup an office layout in the scenario.
/// A desk prefab is attached via the unity editor for instantiation.
/// A coordinate system is also attached via the editor.
/// Desk positions are specified, and these get instantiated to the screen
/// when in-game.
public class OfficeLayout : MonoBehaviour
{
	public CoordinateSystem coordinateSystem;

    public Vector2[] desks;                          // Default to having four desks in these positions.
    public Vector2 suggestionBoxPos;

    public GameObject suggestionBox;

    public List<GameObject> instantiatedDeskList = new List<GameObject>();
    public GameObject instantiatedSuggestionBox;

    public List<Vector2> freeDesks;

    /*
     * Desk offset in terms of base coordinates.
     */
	public Vector3 deskOffset;
	public GameObject desk;

    public Vector2 GetDeskNPCPosition(Vector2 deskPosition)
    {
        return new Vector2(deskPosition.x - 0.5f, deskPosition.y);
    }

    // Returns true when there is a desk available (eg can hire another worker).
    public bool DeskAvailable()
    {
        return freeDesks.Count > 0;
    }
    
    // TODO: update this here to something better.
    public Vector2 GetRandomFreeDeskPosition()
    {
        if (freeDesks.Count == 0)
        {
            // TODO disable buttons etc.
            throw new Exception("Cannot hire employee - not enough desks.");
        }
        else
        {
            Vector2 pos = freeDesks[Random.Range(0, freeDesks.Count)];
            freeDesks.Remove(pos);
            return pos;
        }
    }

    public void DeskTeardown(){
        foreach(GameObject desk in instantiatedDeskList){
            Destroy(desk);
        }

        Destroy(instantiatedSuggestionBox);
    }

    public void DeskSetup(){
        for (int i = 0; i < desks.Length; i++)
        {
            Vector3 pos = coordinateSystem.getVector3(desks[i]);
			instantiatedDeskList.Add(Instantiate(desk, deskOffset + pos, Quaternion.identity));
        }

        // also instantiate the suggestion box
        instantiatedSuggestionBox = Instantiate(suggestionBox, deskOffset + coordinateSystem.getVector3(suggestionBoxPos), Quaternion.identity);

        // create a list to store all the free desks.
        freeDesks = new List<Vector2>();
        foreach(Vector2 desk in desks)
        {
            freeDesks.Add(desk);
        }
    }

    // Use this for initialization
    void Awake()
	{

    }
    


    // Update is called once per frame
    void Update()
    {

    }
}
