using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Task
{

    public enum State
    {
        Available,
        Unavailable,
        InProgress
    }
    public State state;

    // The position where the task can be done. Could also be an array if theres multiple places to complete the task.
    [HideInInspector]
    public Transform destination;

    // The worker associated with this task.
    protected Worker myWorker;

    protected Objective[] allObjectives;

    [Header("Work Time")]
    public float minimumWorkTime;
    private float workProgress;

    public virtual void Setup()
    {
        // Looks for all objectives in the scene, could add a radius around the current entity in here by adding the entity as an overload to this method.
        allObjectives = Object.FindObjectsOfType<Objective>();
    }

    // Function that gets called on the worker which needs to execute the task;
    public virtual void StartTask(Worker worker)
    {
        myWorker = worker;
        state = State.InProgress;

        myWorker.action_GetResources = new Action_GetResources();
        myWorker.action_DepositResources = new Action_DepositResources();
    }

    // Loops through all the objectives to see if theres an objective for this task.
    // If there is, set the destination of this task to the objective
    protected bool GetDestination(Objective.ObjectiveType type)
    {
        foreach (Objective objective in allObjectives)
        {
            if (objective.type == type)
            {
                destination = objective.transform;
                return true;
            }
        }

        return false;
    }

    // Fills a 'loading bar' which represents the worker doing his work. When the workProgress reaches the minimumWorkTime his work is finished and CompleteTask() gets called.
    public IEnumerator DoTask()
    {
        myWorker.workProgressObject.SetActive(true);

        while (workProgress < minimumWorkTime)
        {
            workProgress += Time.deltaTime;
            myWorker.workProgressFill.fillAmount = workProgress / minimumWorkTime;

            // Rotates the 'loading bar' to always face the camera.
            myWorker.workProgressObject.transform.LookAt(Camera.main.transform);

            yield return null;
        }

        CompleteTask();
    }

    // Completes the task, disables the 'loading bar'.
    public virtual void CompleteTask()
    {
        workProgress = 0;
        myWorker.workProgressObject.SetActive(false);
    }
}
