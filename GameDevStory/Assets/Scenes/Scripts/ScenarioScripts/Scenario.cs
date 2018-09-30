using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour{

    public AScenarioExecutor executor;

    public AScenarioCondition condition;

    public ScenarioType type;

    // Control variable indicating if ANY scenario is in progress
    private static bool active = false;

    // Control variable for if the given scenario's status
    private ScenarioStatus status = ScenarioStatus.INCOMPLETE;

    // Returns double between 0 and 1 which indicates probability scenario should be started each game update
    public double GetScenarioProbability()
    {
        return condition.getProbability();
    }

    public void StartScenario()
    {
        status = ScenarioStatus.IN_PROGRESS;
        active = true;
        if(type != null && type == ScenarioType.NOTIFICATION){
            // Send scenario to NPC manager for notification popup and execution
            NPCController.Instance.ShowScenarioNotification(this);
        }else{
            ExecuteScenario();
        }
        
    }

    public void ExecuteScenario()
    {
        executor.execute();
        status = ScenarioStatus.COMPLETE;
        active = false;
    }

    public static bool getActive(){
        // Method to see if any scenario is currently active
        return active;
    }

    public ScenarioStatus getStatus(){
        return status;
    }

}

public enum ScenarioStatus
{
    INCOMPLETE, COMPLETE, IN_PROGRESS
}

public enum ScenarioType{
    INSTANT, NOTIFICATION
}
