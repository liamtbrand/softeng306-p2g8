using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// An action to perform on the npc
public abstract class NPCAction {

    protected delegate void Callback();

    abstract void DoActionHook();

    void DoAction(Callback callback) {
        DoActionHook();
        callback();
    }

}
