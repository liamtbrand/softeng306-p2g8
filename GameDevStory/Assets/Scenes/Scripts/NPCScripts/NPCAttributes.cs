using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttributes : MonoBehaviour {

    public enum Gender { MALE, FEMALE }

    public string npcName;

    public int age;
    public Gender gender;
    public string biography;
    public RuntimeAnimatorController animationController;
   
}
