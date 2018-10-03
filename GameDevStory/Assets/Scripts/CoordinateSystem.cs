using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// The coordinate system forms the basis of the game world.
/// Using a coordinate system allows the game world to be thought of
/// in two dimensions, which makes it easier to think about movement in terms
/// of the office floor.
/// The coordinate system maps into 3d space, where the z axis represents
/// depth in the screen. By positioning the x and y vectors to be in the z axis,
/// animations and sprites automatically go in front of and behind eachother.
/// The origin specifies the root of the coordinate system in terms of the
/// unity display coordinates. (Center the origin at the origin tile's center.)
/// This is used to offset the coordinate system, and position the origin tile.
/// x and y are simply the unit vectors for the coordinate system in terms of
/// the unity display coordinates. These should be positioned such that moving
/// along 1 unit in the x or y direction moves the sprite 1 tile in the game.
/// The position of desks, and players in the game world should be specified
/// in the 2d plane, and the function getVector3 should be used to map these
/// objects onto the display coordinate system.
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
