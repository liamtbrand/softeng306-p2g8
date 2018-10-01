using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoordinateSystem : MonoBehaviour
{
    
    public Vector3 origin;
    
    public Vector3 x;
	public Vector3 y;

    public Vector3 getVector3(Vector2 vector)
    {
        return new Vector3(
          origin.x + vector.x * x.x + vector.y * y.x,
          origin.y + vector.x * x.y + vector.y * y.y,
          origin.z + vector.x * x.z + vector.y * y.z
        );
    }

}
