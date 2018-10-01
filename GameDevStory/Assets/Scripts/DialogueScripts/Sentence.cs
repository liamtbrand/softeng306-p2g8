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

        public UnityAction[] sentenceChoiceActions;

    }
}