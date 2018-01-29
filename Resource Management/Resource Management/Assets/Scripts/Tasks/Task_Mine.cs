using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_Mine : Task
{

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        if (!GetDestination(Objective.ObjectiveType.Mine))
        {
            state = State.Unavailable;
            Debug.LogError("No objective of type 'Mine' found!");
        }

        // Setting the time required to complete this task.
        // I know.. its hardcoded. I just didnt want to create a special script for this or put this somewhere else \o/.
        minimumWorkTime = 5;
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        myWorker.agent.SetDestination(destination.position);
    }

    public override void CompleteTask()
    {
        base.CompleteTask();

        // If the worker has a pickaxe theres a 50% chance to get two ores.
        if (myWorker.inventory.ContainsSpecificItem("pickaxe"))
        {
            myWorker.inventory.AddSpecificItem("ore", Random.Range(1, 3));
        }
        else
        {
            myWorker.inventory.AddSpecificItem("ore", 1);
        }

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
