using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DialogueScripts{
    public class DialogueManager : Singleton<DialogueManager> {

        //todo implement ability to select choices

        private Queue<Sentence> _dialogueQueue;

        public Text NameText;
        public Text DialogueText;
        public Text[] OptionTextArray;
        public Button[] OptionButtonArray;
        public Button ContinueButton;
        public GameObject DialoguePanel;

        protected DialogueManager() { }
        
        void Start () {
            _dialogueQueue = new Queue<Sentence>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
            }

            foreach(Button button in OptionButtonArray){
                button.gameObject.SetActive(false);
            }

            NameText.text = dialogue.Title;

            _dialogueQueue.Clear();
            foreach(Sentence sentence in dialogue.Sentences)
            {
                _dialogueQueue.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void QueueDialogue(Dialogue dialogue){
            // todo sort this shit out
            if(_dialogueQueue.Count == 0){
                StartDialogue(dialogue);
            }else{
                foreach(Sentence sentence in dialogue.Sentences)
                {
                    _dialogueQueue.Enqueue(sentence);
                }
            }



        }

        public void DisplayNextSentence()
        {
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

            DialogueText.text = sentence.sentenceLine;

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

        public void EndDialogue()
        {
            DialoguePanel.SetActive(false);
        }

    }
}