using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script containing state & controlling behaviour for an individual NPC
public class NPCBehaviour : MonoBehaviour
{

    // Enum for animation states of the NPC Controllers
    public enum State { IDLE = 0, WALKING = 1, CODING = 2, DANCING = 3};

	public State state;

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

    // Action to trigger state change the NPC in question
    public void SetState(State state)
    {
        // Get the animator for the specified NPC
        Animator animator = this.GetComponent<Animator>();

		this.state = state;

        // Set the state of the animator, in accordance with the animator
        // overridden controller finite state machine
		animator.SetInteger("State", (int)state);
    }

    // Getter method for NPC animation state
    public State GetState()
	{
		return this.state;
	}
}
