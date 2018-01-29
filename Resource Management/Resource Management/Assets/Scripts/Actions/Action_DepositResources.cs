using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_DepositResources : BaseAction
{

    public override void Setup()
    {
        base.Setup();

        destination = GameObject.FindWithTag("ResourceBase").transform;
    }

    public override void DoAction(Worker worker)
    {
        base.DoAction(worker);

        worker.agent.SetDestination(destination.position);
    }

    public override void CompleteAction(Worker worker)
    {
        base.CompleteAction(worker);

        // Cache the workers inventory, keeps it a bit more clear.
        Inventory workerInv = worker.inventory;

        // Checks if the worker has resources in his inventory and deposits them is he has.
        for (int i = 0; i < workerInv.items.Count; i++)
        {
            // Every worker should have a pickaxe in his inventory, this checks if the worker has more than one.
            // If he has more than one, deposit them.
            if (workerInv.items[i].itemName == "pickaxe")
            {
                if (workerInv.items[i].quantity > 1)
                {
                    // Add to resource base inventory.
                    ResourceManager.instance.inventory.AddSpecificItem(workerInv.GetSpecificItem("pickaxe").itemName, workerInv.GetSpecificItem("pickaxe").quantity - 1);
                    // Remove from worker inventory.
                    workerInv.RemoveSpecificItem("pickaxe", workerInv.GetSpecificItem("pickaxe").quantity - 1);
                    break;
                }

                break;
            }

            // This just deposits every other item.
            ResourceManager.instance.inventory.AddSpecificItem(workerInv.items[i].itemName, workerInv.items[i].quantity);
            workerInv.RemoveSpecificItem(workerInv.items[i].itemName, workerInv.items[i].quantity);
        }

        UIManager.instance.UpdateResourcePanel();
    }
}
