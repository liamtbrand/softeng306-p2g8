using UnityEngine;
using System;

public class OfficeLayout : MonoBehaviour
{
	public CoordinateSystem coordinateSystem;

	public static Vector2[] desks = {
        new Vector2(2,1),
        new Vector2(3,1),
        new Vector2(3,3)
    };

    /*
     * Desk offset in terms of base coordinates.
     */
	public Vector3 deskOffset;
	public GameObject desk;

    // Use this for initialization
    void Start()
	{
    
		for (int i = 0; i < desks.Length; i++)
        {
            Vector3 pos = coordinateSystem.getVector3(desks[i]);
			Instantiate(desk, deskOffset + pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
