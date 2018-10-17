using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueScripts{
    
    [System.Serializable]
    public class Dialogue {

        //todo configure text sizing
        public Sentence[] Sentences;
        
        // properties that are used when dialogue has monetary consequences
        public int RewardAmount;
        public int PenaltyAmount;

    }
}