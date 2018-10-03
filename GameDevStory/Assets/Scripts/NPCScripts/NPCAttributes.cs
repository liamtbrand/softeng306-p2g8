using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// data structure defining NPC attributes - should be initialized in the unity editor
public class NPCAttributes : MonoBehaviour {
    
    public enum Gender { MALE, FEMALE }
    public string npcName;
    public int age;
    public Gender gender;
    public string biography;
    public RuntimeAnimatorController animationController;
    public int cost;

}
