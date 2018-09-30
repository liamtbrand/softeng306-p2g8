using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectSelector : MonoBehaviour {

	public GameObject projectEntry;
	public GameObject projectEntryListParent;

	public void SetupProjectMenu () 
	{
		GameObject newEntry = Instantiate (projectEntry, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;
		newEntry.transform.SetParent (projectEntryListParent.transform);
	}

}
