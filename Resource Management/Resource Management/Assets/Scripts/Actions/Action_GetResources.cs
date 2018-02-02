using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetResources : BaseAction
{

    public List<Item> resourcesToGet = new List<Item>();

    public override void Setup()
    {
        base.Setup();

        destination = GameObject.FindWithTag("ResourceBase").transform;
    }

    public override void DoAction(Worker worker)
    {
        base.DoAction(worker);

        // Tells the worker to move to a certain position.
        worker.agent.SetDestination(destination.position);
    }

    public override void CompleteAction(Worker worker)
    {
        base.CompleteAction(worker);

        // Another check to see if the resource inventory has all the needed items.
        // If not, get a new task.
        for (int i = 0; i < resourcesToGet.Count; i++)
        {
            if (!ResourceManager.instance.inventory.ContainsSpecificItem(resourcesToGet[i].itemName, resourcesToGet[i].quantity))
            {
                TaskManager.instance.GetNewTask(worker);
                return;
            }
        }

        // Grabs all of the necessary resources out of the resource base and adds them to the workers inventory.
        for (int i = 0; i < resourcesToGet.Count; i++)
        {
            // Add to worker inventory.
            worker.inventory.AddSpecificItem(resourcesToGet[i].itemName, resourcesToGet[i].quantity);

            // Remove from resource base.
            ResourceManager.instance.inventory.RemoveSpecificItem(resourcesToGet[i].itemName, resourcesToGet[i].quantity);
        }

        // Updates the UI that shows how much resources we have.
        UIManager.instance.UpdateResourcePanel();
    }
}
