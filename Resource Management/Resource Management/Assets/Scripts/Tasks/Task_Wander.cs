using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_Wander : Task
{

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        //if (!GetDestination(Objective.ObjectiveType.Mine))
        //{
        //    state = State.Unavailable;
        //    Debug.LogError("No objective of type 'Mine' found!");
        //}
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        worker.currentJobDescription = "Wandering Around";
    }
}
