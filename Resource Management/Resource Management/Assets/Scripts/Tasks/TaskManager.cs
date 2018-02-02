using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static TaskManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GetNewTask(Worker worker)
    {
        // Reference to the ResourceManager inventory.
        Inventory resourceBaseInv = ResourceManager.instance.inventory;

        // The very first check should be the workers stats.
        // Start by checking the workers hunger.
        if (worker.stats.hunger.currentAmount < 50)
        {
            // Check if the worker has bread in his inventory, if he has then eat it and give him a new task.
            if (worker.inventory.ContainsSpecificItem("bread", 1))
            {
                // Eat the bread (remove from inventory, refill health, give him a new task)
                worker.inventory.GetSpecificItem("bread").quantity--;
                worker.stats.hunger.currentAmount = worker.stats.hunger.maxAmount;
                GetNewTask(worker);
                return;
            }
            // Else if the worker has no bread check if the resource base has bread.
            // If theres bread in the resource base the worker should go and get it.
            else if (resourceBaseInv.ContainsSpecificItem("bread", 1))
            {
                worker.action_GetResources = new Action_GetResources();
                worker.action_GetResources.Setup();

                // If the worker is already at the right place, give him some bread.
                if (Vector3.Distance(worker.transform.position, worker.action_GetResources.destination.position) < 25f)
                {
                    // Prepares the action to get resources (tells the action that it should fetch bread).
                    worker.action_GetResources.resourcesToGet.Add(new Item("bread", 1));

                    //Completes the action.
                    worker.action_GetResources.CompleteAction(worker);

                    // Calls this function again so it goes through the above checks again but this time the worker has bread in his inventory.
                    GetNewTask(worker);
                }
                // If not, walk to the right place.
                else
                {
                    worker.action_GetResources.DoAction(worker);
                }

                return;
            }
            // If theres no bread at all, check if theres grain in the resource base. If there is the worker should go and make some bread.
            else if (resourceBaseInv.ContainsSpecificItem("grain", ResourceManager.instance.breadGrainCost))
            {
                worker.SetTask(Worker.State.Baking);
                return;
            }
            // If theres literally nothing the worker can eat then he should go and harvest grain to be able to make bread.
            else
            {
                worker.SetTask(Worker.State.HarvestingGrain);
                return;
            }
        }

        // After the workers stat check, the tool supply gets checked.
        // Starting with checking if we have a pickaxe since it improves the collection of resources.
        if (!worker.inventory.ContainsSpecificItem("pickaxe"))
        {
            // If the worker doesnt have a pickaxe, check if we have resources to make one.
            // If we do, make a pickaxe!
            if (resourceBaseInv.ContainsSpecificItem("wood", ResourceManager.instance.pickaxeWoodCost) && resourceBaseInv.ContainsSpecificItem("ore", ResourceManager.instance.pickaxeOreCost))
            {
                worker.SetTask(Worker.State.Smithing);
                return;
            }
        }

        // If the wood supply is bigger than the ore supply, go mine for ore.
        if (resourceBaseInv.GetSpecificItem("wood").quantity > resourceBaseInv.GetSpecificItem("ore").quantity)
        {
            worker.SetTask(Worker.State.Mining);
            return;
        }
        // Else if the ore supply is bigger than the wood supply, go chop some trees.
        else if (resourceBaseInv.GetSpecificItem("ore").quantity > resourceBaseInv.GetSpecificItem("wood").quantity)
        {
            worker.SetTask(Worker.State.ChoppingTrees);
            return;
        }
        // Else if the supplies are all equal, pick a random task.
        else
        {
            int randomTask = Random.Range(0, 2);

            if (randomTask == 0)
            {
                worker.SetTask(Worker.State.ChoppingTrees);
            }
            else if (randomTask == 1)
            {
                worker.SetTask(Worker.State.Mining);
            }
        }
    }
}
