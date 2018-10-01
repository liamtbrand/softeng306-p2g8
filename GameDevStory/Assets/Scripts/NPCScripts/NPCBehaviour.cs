    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    // script containing state & controlling behaviour for an individual NPC
    public class NPCBehaviour : MonoBehaviour {

    	// Enum for animation states of the NPC Controllers
        public enum State { IDLE, WALKING, CODING };

        private const float NOTIFICATION_HEIGHT_OFFSET = 0.22f; //todo adjust the scale of the world so we don't need to deal in tiny numbers
        private bool hasNotification = false; // so we know when this NPC is available to show a notification

        public GameObject notificationButton; // the button to show when an event occurs for this npc

        void Start()
        {
            Debug.Log("NPC Script initializing");
            //todo some stuff will surely go here when this gets more complex
        }

        public bool HasNotification()
        {
            return this.hasNotification;
        }

        public void ShowScenarioNotification(Scenario s)
        {
            Debug.Log("Showing Scenario Notification!");
            this.hasNotification = true;

            // show the notification button in the scene
            Vector3 npcCurrentPosition = transform.position;
            GameObject buttonInstanceContainer = Instantiate(notificationButton, new Vector3(npcCurrentPosition.x, npcCurrentPosition.y + NOTIFICATION_HEIGHT_OFFSET, 0f), Quaternion.identity);
            buttonInstanceContainer.transform.SetParent(this.transform);

            // access the button part of the notification component and register the scenario to be executed on click
            Button buttonInstance = buttonInstanceContainer.GetComponentInChildren<Button>();
            buttonInstance.onClick.AddListener(() => {
                s.ExecuteScenario();
                this.hasNotification = false;
                Debug.Log("Click!");
                Destroy(buttonInstanceContainer); // could set a delay as second param if desired
            });
        }
       
    /*
     * WIP!!! Still needs to be indented and coded (well), most of the coding wrt the changing of animations is fine,
     * it's the state diagrams of the animation override controller that needs to be fucked around with
     */

       // Action to trigger state change of a given NPC (or randomly chosen, if index is < 0)       public void Action(State state)       {                   // Get the animator for the specified NPC            Animator animator = this.GetComponent<Animator>();            if (state == State.IDLE)            {                animator.SetInteger("State", 1);            }
       }


    }
