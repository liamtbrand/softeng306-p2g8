using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueScripts{
    [System.Serializable]
    public class Sentence {

        public string Title;

        public Sprite icon;

        [TextArea(3, 10)]
        public string sentenceLine;

        public string[] sentenceChoices;

        // an alternative way of providing sentence choices for when there is a right and a wrong answer
        public BooleanSentenceChoice[] booleanChoices;

        // one-to-one mapping of result strings and choices made (e.g booleanChoices[0] triggers results[0]
		public string[] results;

        public UnityAction[] sentenceChoiceActions;

    }
}