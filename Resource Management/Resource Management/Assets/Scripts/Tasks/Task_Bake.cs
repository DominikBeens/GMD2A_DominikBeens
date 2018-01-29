using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_Bake : Task
{

    private int breadGrainCost;

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        if (!GetDestination(Objective.ObjectiveType.Bakery))
        {
            state = State.Unavailable;
            Debug.LogError("No objective of type 'Bakery' found!");
        }

        // Sets the grain cost to make one bread.
        breadGrainCost = ResourceManager.instance.breadGrainCost;

        // Setting the time required to complete this task.
        // I know.. its hardcoded. I just didnt want to create a special script for this or put this somewhere else \o/.
        minimumWorkTime = 12;
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        if (myWorker.inventory.ContainsSpecificItem("grain", breadGrainCost))
        {
            myWorker.agent.SetDestination(destination.position);
        }
        else if (ResourceManager.instance.inventory.ContainsSpecificItem("grain", breadGrainCost))
        {
            myWorker.action_GetResources.Setup();

            if (Vector3.Distance(myWorker.transform.position, destination.position) < 25f)
            {
                // One bread costs a certain amount of grain to make, this function divides the amount of grain we have with the amount of grain needed to make one bread.
                // The worker takes that amount of grain and makes as much bread as he can make with it.
                // The list gets cleared first because we dont want to add even more grain if we call this function again.
                myWorker.action_GetResources.resourcesToGet.Clear();
                for (int i = 0; i < ResourceManager.instance.inventory.GetSpecificItem("grain").quantity / breadGrainCost; i++)
                {
                    myWorker.action_GetResources.resourcesToGet.Add(new Item("grain", breadGrainCost));
                }

                myWorker.action_GetResources.CompleteAction(myWorker);
                myWorker.agent.SetDestination(destination.position);
            }
            else
            {
                myWorker.action_GetResources.DoAction(myWorker);
            }
        }
        else
        {
            TaskManager.instance.GetNewTask(myWorker);
        }
    }

    // Worker finished making bread, remove the cost and give him the bread in his inventory.
    public override void CompleteTask()
    {
        base.CompleteTask();

        myWorker.inventory.RemoveSpecificItem("grain", breadGrainCost);

        myWorker.inventory.AddSpecificItem("bread", 1);

        // If the worker has enough resources to make another bread, make another one. If not then go and deposit the resources.
        if (myWorker.inventory.ContainsSpecificItem("grain", breadGrainCost))
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
