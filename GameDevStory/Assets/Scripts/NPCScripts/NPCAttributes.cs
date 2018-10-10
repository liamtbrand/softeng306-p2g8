using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// When generating NPCs, they need to be consistent in terms of naming, look,
/// gender, biography, etc.
/// This is because we do not want to create incoherent characters.
/// To acomplish this, the datastructure NPC Attributes is initialised
/// from within the unity editor with data that is coherent.
/// This data is loaded into the NPCFactory for consumption.
public class NPCAttributes : MonoBehaviour {

    public enum Gender { MALE, FEMALE }
    public string npcName;
    public int age;
    public Gender gender;
    [TextArea]
    public string biography;
    public RuntimeAnimatorController animationController;
    public int cost;
    public int costThreshold;
    public Sprite headshot;     // for when this npc has dialogue

}
