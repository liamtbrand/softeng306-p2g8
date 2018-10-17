using DialogueScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// manages showing notifications above the suggestion box in the scene and
// maintains a queue of scenarios that can occur at any point in game. These
// scenarios are made by anonymous employees
public class SuggestionBoxManager : Singleton<SuggestionBoxManager> {

    public Sprite AnonymousHeadShot;
    public List<Dialogue> DialoguePool; // possible dialogues that can appear
    public GameObject NotificationButton; // the button to show above the suggestion box
    public float DialogueProbability; // probability that one of the dialogues will be started

    private GameObject suggestionBox;
    private readonly Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    private bool active = false;
    private Vector3 notificationPosition;
    private List<Dialogue> dialoguePoolCopy; // working copy for each instance of suggestion box

    private const float NOTIFICATION_HEIGHT_OFFSET = 0.14f;
    private const float NOTIFICATION_X_OFFSET = -0.01f;


    // Use this for initialization
    void Start() {
        Debug.Log("suggestion box is live");
        InitSuggestionBoxManager();
    }

    // Update is called once per frame
    void Update() {
        // scenario will only show up if one is not already showing and the queue is non-empty
        if (!active && dialogueQueue.Count != 0 && Random.Range(0.0f, 1.0f) < DialogueProbability)
        {
            active = true;

            // get next dialogue in queue and set the icon of each sentence to be the anonymous person
            Dialogue dialogue = dialogueQueue.Dequeue();
            foreach (Sentence s in dialogue.Sentences)
            {
                // initialise the actions for the sentence if it has choices
                // assuming that there are exactly two options
                if (s.booleanChoices.Length != 0)
                {
                    s.sentenceChoiceActions = new UnityAction[2];
                    s.sentenceChoices = new string[2];
                    // set the sentence choices and their actions depending if they were correct or incorrect
                    for (int i = 0; i < 2; i++)
                    {
                        s.sentenceChoices[i] = s.booleanChoices[i].sentenceChoice;
                        if (s.booleanChoices[i].isCorrectChoice)
                            s.sentenceChoiceActions[i] = delegate ()
                            {
                                GameManager.Instance.changeBalance(dialogue.RewardAmount);
                                Sentence outcome = dialogue.Sentences[dialogue.Sentences.Length - 1];
							    outcome.sentenceLine = dialogue.Sentences[dialogue.Sentences.Length - 1].results[0];
                                outcome.sentenceChoices = new string[] { "Okay" };
                                outcome.sentenceChoiceActions = new UnityAction[] {
                                    delegate ()
                                    {
                                        ProjectManager.Instance.ResumeProject();
                                        // remove from the global queue once complete so it won't occur twice
                                        DialoguePool.Remove(dialogue);
                                    }
                                };
                                
                            };
                        else
                            s.sentenceChoiceActions[i] = delegate ()
                            {
                                GameManager.Instance.changeBalance(-1 * dialogue.PenaltyAmount);
                                Sentence outcome = dialogue.Sentences[dialogue.Sentences.Length - 1];
                                outcome.sentenceLine = dialogue.Sentences[dialogue.Sentences.Length - 1].results[1];
                                outcome.sentenceChoices = new string[] { "Okay" };
                                outcome.sentenceChoiceActions = new UnityAction[] {
                                    delegate ()
                                    {
                                        ProjectManager.Instance.ResumeProject();
                                        // remove from the global queue once complete so it won't occur twice
                                        DialoguePool.Remove(dialogue);
                                    }
                                };
                            };
                    }
                }

                // if no icon is specified assume anonymous
                if (s.icon == null)
                    s.icon = AnonymousHeadShot;

            }

            Debug.Log("showing suggestion box notification");
            // show notification above suggestion box
            GameObject button = Instantiate(NotificationButton, notificationPosition, Quaternion.identity);
            button.transform.SetParent(suggestionBox.transform);
            Debug.Log(notificationPosition);
            button.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                active = false;
                DialogueManager.Instance.StartDialogue(dialogue);
                ProjectManager.Instance.PauseProject();
                Destroy(button);
            });

        }
    }

    // notifies this script that it needs to update its reference to the suggestion box
    // and reset state
    public void SceneWasSwitched()
    {
        InitSuggestionBoxManager();
        active = false;
    }

    private void InitSuggestionBoxManager()
    {
        dialogueQueue.Clear();

        // get reference to the in-game suggestion box
        suggestionBox = LevelManager.Instance.GetCurrentLevel().GetOfficeLayout().instantiatedSuggestionBox;

        // calculate position for notification button
        Vector3 boxPos = suggestionBox.transform.position;
        notificationPosition = new Vector3
        {
            x = boxPos.x + NOTIFICATION_X_OFFSET,
            y = boxPos.y + NOTIFICATION_HEIGHT_OFFSET,
            z = boxPos.z + 1
        };

        if (DialoguePool.Count == 0)
            return;

        // use the copy so we don't remove scenarios that should persist when scene is changed
        dialoguePoolCopy = new List<Dialogue>(DialoguePool);

        // set the first element in the dialogue pool, to be the head of queue (it MUST come first)

        dialogueQueue.Enqueue(dialoguePoolCopy[0]);
        dialoguePoolCopy.RemoveAt(0);

        // fill up the queue in random order
        int N = dialoguePoolCopy.Count;
        for (int i = 0; i < N; i++)
        {
            int randomIndex = Random.Range(0, dialoguePoolCopy.Count);
            dialogueQueue.Enqueue(dialoguePoolCopy[randomIndex]);
            dialoguePoolCopy.RemoveAt(randomIndex);
        }
    }
}
