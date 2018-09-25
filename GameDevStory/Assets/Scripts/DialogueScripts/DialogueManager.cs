using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager> {

    //todo implement ability to select choices

    private Queue<string> _dialogueQueue;

    public Text NameText;
    public Text DialogueText;
    public GameObject DialoguePanel;

    protected DialogueManager() { }
	
	void Start () {
        _dialogueQueue = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        if (!DialoguePanel.activeInHierarchy)
        {
            DialoguePanel.SetActive(true);
        }

        NameText.text = dialogue.Title;

        _dialogueQueue.Clear();
        foreach(string sentence in dialogue.Sentences)
        {
            _dialogueQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueText.text = _dialogueQueue.Dequeue();

    }

    public void EndDialogue()
    {
        DialoguePanel.SetActive(false);
    }

}
