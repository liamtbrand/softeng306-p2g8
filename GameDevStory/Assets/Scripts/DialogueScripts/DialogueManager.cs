using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DialogueScripts{
    public class DialogueManager : Singleton<DialogueManager> {

        private Queue<Sentence> _dialogueQueue;

        public bool DialogueInProgress = false;

        public GameObject DialogueContainer;

        public Text NameText;
        public Text DialogueText;
        public Text[] OptionTextArray;
        public Button[] OptionButtonArray;
        public Button ContinueButton;
        public GameObject DialoguePanel;

        public SpriteRenderer NPCIcon;

        protected DialogueManager() { }
        
        void Awake () {
            _dialogueQueue = new Queue<Sentence>();
            
        }

        public void StartDialogue(Dialogue dialogue)
        {
            DialogueInProgress = true;

            Debug.Log("Start dialogue");
            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
            }

            foreach(Button button in OptionButtonArray){
                button.gameObject.SetActive(false);
            }
            
            foreach(Sentence sentence in dialogue.Sentences)
            {
                Debug.Log("Queueing dialogue");
                _dialogueQueue.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void QueueDialogue(Dialogue dialogue){
            Debug.Log("Queueing dialogue");

            foreach(Sentence sentence in dialogue.Sentences)
            {
                Debug.Log("Queueing dialogue");
                _dialogueQueue.Enqueue(sentence);
            }
            
        }

        public void DisplayNextSentence()
        {

            if (!DialoguePanel.activeInHierarchy)
            {
                    DialoguePanel.SetActive(true);
            }

            Debug.Log("Displaying next sentence");
            foreach(Button button in OptionButtonArray){
                button.gameObject.SetActive(false);
            }

            ContinueButton.gameObject.SetActive(false);

            if(_dialogueQueue.Count == 0)
            {
                EndDialogue();
                return;
            }

            Sentence sentence = _dialogueQueue.Dequeue();

            //DialogueText.text = sentence.sentenceLine;

            
            NameText.text = sentence.Title;

            NPCIcon.sprite = sentence.icon;

            StopAllCoroutines();
		    StartCoroutine(TypeSentence(sentence.sentenceLine));

            if(sentence.sentenceChoices != null && sentence.sentenceChoices.Length > 0){
                Debug.Log("Generating " + sentence.sentenceChoices.Length + " choice buttons");
                for(int i = 0; i < sentence.sentenceChoices.Length && i < OptionButtonArray.Length && i < OptionTextArray.Length; i++){
                    OptionButtonArray[i].gameObject.SetActive(true);
                    OptionButtonArray[i].onClick.RemoveAllListeners();
                    OptionButtonArray[i].onClick.AddListener(sentence.sentenceChoiceActions[i]);
                    UnityAction continueOnButtonClick = DisplayNextSentence;
                    OptionButtonArray[i].onClick.AddListener(continueOnButtonClick);
                    OptionTextArray[i].text = sentence.sentenceChoices[i];
                }
            }else{
                ContinueButton.gameObject.SetActive(true);
            }
            

        }

        private IEnumerator TypeSentence(string sentenceLine){
            DialogueText.text = "";
		    foreach (char letter in sentenceLine.ToCharArray())
		    {
			    DialogueText.text += letter;
			    yield return null;
		    }
        }

        public void EndDialogue()
        {
            Debug.Log("Ending dialogue");

            DialogueInProgress = false;
            
            if(_dialogueQueue.Count == 0)
            {
                DialoguePanel.SetActive(false);
            }
        }

    }
}