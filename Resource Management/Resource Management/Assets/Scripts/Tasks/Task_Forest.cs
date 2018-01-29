using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task_Forest : Task
{

    private GameObject closestTree;

    public override void Setup()
    {
        base.Setup();

        // After the GetDestination() foreach loop has finished (see base for explanation) and the destination is still null it means that the game didnt find an objective of the corresponding type.
        // So it sets the state to unavailable and throws an error!
        if (!GetDestination(Objective.ObjectiveType.Forest))
        {
            state = State.Unavailable;
            Debug.LogError("No objective of type 'Forest' found!");
        }

        // Setting the time required to complete this task.
        // I know.. its hardcoded. I just didnt want to create a special script for this or put this somewhere else \o/.
        minimumWorkTime = 5;
    }

    public override void StartTask(Worker worker)
    {
        base.StartTask(worker);

        GetClosestTree();
        Debug.Log(closestTree.name);

        if (Vector3.Distance(myWorker.transform.position, closestTree.transform.position) < 1f)
        {
            DoTask();
            return;
        }

        myWorker.agent.SetDestination(closestTree.transform.position);
    }

    // Find all trees with a certain tag, loops through all of them and checks the distance from the worker to the tree and saves the closest one.
    private void GetClosestTree()
    {
        GameObject[] allTrees = GameObject.FindGameObjectsWithTag("Tree");

        for (int i = 0; i < allTrees.Length; i++)
        {
            if (closestTree == null)
            {
                if (allTrees[i].GetComponent<Objective>().availability == Objective.Availability.Available)
                {
                    closestTree = allTrees[i];
                }
            }

            // If the distance between the worker and the current tree is less than the distance between the worker and the already closest tree. 
            if (Vector3.Distance(myWorker.transform.position, allTrees[i].transform.position) < Vector3.Distance(myWorker.transform.position, closestTree.transform.position))
            {
                if (allTrees[i].GetComponent<Objective>().availability == Objective.Availability.Available)
                {
                    // Assign a new closest tree.
                    closestTree = allTrees[i];
                }
            }
        }
    }

    // The task has been completed, adds certain items as rewards.
    public override void CompleteTask()
    {
        base.CompleteTask();

        // Tree has been cut down, display a grow animation.
        //closestTree.GetComponent<Animator>().SetTrigger("Grow");
        Object.Destroy(closestTree);

        myWorker.inventory.AddSpecificItem("wood", 1);

        // A little randomizer where the worker either goes to work again or goes to deposit his earned items.
        int randomizer = Random.Range(0, 2);
        if (randomizer == 0 || randomizer == 1)
        {
            TaskManager.instance.StartCoroutine(ChopNextTree());
        }
        else
        {
            myWorker.action_DepositResources.Setup();
            myWorker.action_DepositResources.DoAction(myWorker);

            state = State.Available;
        }
    }

    private IEnumerator ChopNextTree()
    {
        yield return new WaitForSeconds(2f);
        myWorker.SetTask(Worker.State.ChoppingTrees);
    }
}
