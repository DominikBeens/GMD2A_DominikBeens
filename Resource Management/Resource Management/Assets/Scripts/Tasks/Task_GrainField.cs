using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_GrainField : Task
{

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        if (!GetDestination(Objective.ObjectiveType.GrainField))
        {
            state = State.Unavailable;
            Debug.LogError("No objective of type 'Grain Field' found!");
        }

        // Setting the time required to complete this task.
        // I know.. its hardcoded. I just didnt want to create a special script for this or put this somewhere else \o/.
        minimumWorkTime = 7;
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        myWorker.agent.SetDestination(destination.position);
    }

    // The task has been completed, adds certain items as rewards.
    public override void CompleteTask()
    {
        base.CompleteTask();

        myWorker.inventory.AddSpecificItem("grain", Random.Range(3, 7));

        // A little randomizer where the worker either goes to work again or goes to deposit his earned items.
        int randomizer = Random.Range(0, 2);
        if (randomizer == 0)
        {
            TaskManager.instance.StartCoroutine(DoTask());
        }
        else
        {
            myWorker.action_DepositResources.Setup();
            myWorker.action_DepositResources.DoAction(myWorker);

            state = State.Available;
        }
    }
}
