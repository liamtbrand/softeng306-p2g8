using UnityEngine;

public class OfficeLevelSetupScript : BaseLevelSetupScript
{
    public override void setup()
    {
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.orthographicSize = 1.25f;

    }
}