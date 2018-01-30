using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_Smith : Task
{

    private int pickaxeWoodCost;
    private int pickaxeOreCost;

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        if (!GetDestination(Objective.ObjectiveType.Smith))
        {
            state = State.Unavailable;
            Debug.LogError("No objective of type 'Smith' found!");
        }

        // Sets the cost to create a pickaxe.
        pickaxeWoodCost = ResourceManager.instance.pickaxeWoodCost;
        pickaxeOreCost = ResourceManager.instance.pickaxeOreCost;

        // Setting the time required to complete this task.
        // I know.. its hardcoded. I just didnt want to create a special script for this or put this somewhere else \o/.
        minimumWorkTime = 15;
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        // If the worker has enough resources to make a pickaxe, send him to the smith.
        if (myWorker.inventory.ContainsSpecificItem("wood", pickaxeWoodCost) && myWorker.inventory.ContainsSpecificItem("ore", pickaxeOreCost))
        {
            myWorker.agent.SetDestination(destination.position);
        }
        // Else check if the resource base has all of the needed resources.
        else if (ResourceManager.instance.inventory.ContainsSpecificItem("wood", pickaxeWoodCost) && ResourceManager.instance.inventory.ContainsSpecificItem("ore", pickaxeOreCost))
        {
            // If it has, send the worker to get them.
            myWorker.action_GetResources.Setup();

            // Check if hes already at the resource base.
            if (Vector3.Distance(myWorker.transform.position, destination.position) < 25f)
            {
                // Prepares the action to get resources.
                myWorker.action_GetResources.resourcesToGet.Add(new Item("wood", pickaxeWoodCost));
                myWorker.action_GetResources.resourcesToGet.Add(new Item("ore", pickaxeOreCost));

                // Completes the action and sends him to the smith.
                myWorker.action_GetResources.CompleteAction(myWorker);
                myWorker.agent.SetDestination(destination.position);
            }
            // Else send him there.
            else
            {
                myWorker.action_GetResources.DoAction(myWorker);
            }
        }
        // Else if there is no resources to make a pickaxe anywhere, give him a new task which will probably tell him to gather resources.
        else
        {
            TaskManager.instance.GetNewTask(myWorker);
        }
    }

    // Worker finished making a pickaxe, remove the cost and give him a pickaxe in his inventory.
    public override void CompleteTask()
    {
        base.CompleteTask();

        myWorker.inventory.RemoveSpecificItem("wood", pickaxeWoodCost);
        myWorker.inventory.RemoveSpecificItem("ore", pickaxeOreCost);

        myWorker.inventory.AddSpecificItem("pickaxe", 1);

        // If the worker has enough resources to make another pickaxe, make another one. If not then go and deposit the resources.
        if (myWorker.inventory.ContainsSpecificItem("wood", pickaxeWoodCost) && myWorker.inventory.ContainsSpecificItem("ore", pickaxeOreCost))
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
