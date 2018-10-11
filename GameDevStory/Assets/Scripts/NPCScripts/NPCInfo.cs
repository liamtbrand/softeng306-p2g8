using System.Collections;
using System.Collections.Generic;
using NPCScripts.StaffStateScripts;
using UnityEngine;

// wrapper class to hold all info needed to generate an in-game NPC
public class NPCInfo {

    public NPCAttributes Attributes;
    public NPCFactory.NPCStats Stats;
    public bool IsStaff = true;
    public bool IsAvailableForHire = true;
    public StaffMentalState MentalState = new StaffMentalState();
}
