using DialogueScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manages showing notifications above the suggestion box in the scene and
// maintains a queue of scenarios that can occur at any point in game. These
// scenarios are made by anonymous employees
public class SuggestionBoxManager : MonoBehaviour {

    public Sprite AnonymousHeadShot;
    public List<Dialogue> DialoguePool; // possible dialogues that can appear
    public double RewardAmount; // reward if player makes correct choice
    public double PenaltyAmount; // penalty if player makes incorrect choice
    public GameObject notificationButton; // the button to show above the suggestion box

    private GameObject suggestionBox;
    private readonly float dialogueProbability = 0.01f;  // should be fine-tuned so suggestion box problems pop up rarely, but still a few times per game
    private readonly Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    private bool active = false;
    private Vector3 notificationPosition;

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.14f;
    private const float NOTIFICATION_X_OFFSET = -0.01f;


	// Use this for initialization
	void Start () {
        Debug.Log(" suggestion box is live");
        
        // get reference to the in-game suggestion box
        suggestionBox = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().instantiatedSuggestionBox;

        // fill up the queue in random order
        int N = DialoguePool.Count;
        for (int i = 0; i < N; i++)
        {
            int randomIndex = Random.Range(0, DialoguePool.Count);
            dialogueQueue.Enqueue(DialoguePool[randomIndex]);
            DialoguePool.RemoveAt(randomIndex);
        }

        // calculate position for notification button
        Vector3 boxPos = suggestionBox.transform.position;
        notificationPosition = new Vector3
        {
            x = boxPos.x + NOTIFICATION_X_OFFSET,
            y = boxPos.y + NOTIFICATION_HEIGHT_OFFSET,
            z = boxPos.z + 1
        };

        Debug.Log(boxPos);
        Debug.Log(notificationPosition);
    }
	
	// Update is called once per frame
	void Update () {
		
        // scenario will only show up if one is not already showing and the queue is non-empty
        if (!active && dialogueQueue.Count != 0 && Random.Range(0.0f, 1.0f) < dialogueProbability)
        {
            active = true;

            // get next dialogue in queue and set the icon of each sentence to be the anonymous person
            Dialogue dialogue = dialogueQueue.Dequeue();
            foreach (Sentence s in dialogue.Sentences)
            {
                s.icon = AnonymousHeadShot;
            }

            Debug.Log("showing suggestion box notification");
            // show notification above suggestion box
            GameObject button = Instantiate(notificationButton, notificationPosition, Quaternion.identity);
            button.transform.SetParent(suggestionBox.transform);
            Debug.Log(notificationPosition);
            button.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                active = false;
                DialogueManager.Instance.StartDialogue(dialogue);
                Destroy(button);
            });

        }
	}
}
