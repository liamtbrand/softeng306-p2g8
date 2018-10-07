using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script containing state & controlling behaviour for an individual NPC
public class NPCBehaviour : MonoBehaviour
{

    // Enum for animation states of the NPC Controllers
    public enum State { IDLE, WALKING, CODING };

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f; //todo adjust the scale of the world so we don't need to deal in tiny numbers
    private bool hasNotification = false; // so we know when this NPC is available to show a notification

    void Start()
    {
        Debug.Log("NPC Script initializing");
        //todo some stuff will surely go here when this gets more complex
    }

    public bool GetHasNotification()
    {
        return this.hasNotification;
    }

    public void SetHasNotification(bool hasNotification)
    {
        this.hasNotification = hasNotification;
    }

    /*
     * WIP!!! Still needs to be indented and coded (well), most of the coding wrt the changing of animations is fine,
     * it's the state diagrams of the animation override controller that needs to be fucked around with
     */

    // Action to trigger state change of a given NPC (or randomly chosen, if index is < 0)
    public void Action(State state)
    {
        // Get the animator for the specified NPC
        Animator animator = this.GetComponent<Animator>();

        if (state == State.IDLE)
        {
            animator.SetInteger("State", 1);
        }

    }
}
