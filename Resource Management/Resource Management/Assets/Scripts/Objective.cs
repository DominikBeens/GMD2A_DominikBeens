using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{

    // World space canvas with a text object that displays the name of this objective.
    private Text objectiveText;
    private Animator anim;

    public enum ObjectiveType
    {
        ResourceBase,
        Forest,
        Mine,
        Smith,
        GrainField,
        Bakery
    }
    public ObjectiveType type;

    // Give the posibility to make the objective unavailable. Currently this is only used with trees for when they are 'regrowing'.
    public enum Availability
    {
        Available,
        Unavalable
    }
    public Availability availability;

    private void Awake()
    {
        // Using this method of finding the text since theres only one text element on the objective and im too lazy to assign it in the inspector.
        objectiveText = GetComponentInChildren<Text>();
        anim = GetComponent<Animator>();

        // Sets the text thats displayed at the objective based on what the objectives type is.
        if (objectiveText != null)
        {
            switch (type)
            {
                case ObjectiveType.ResourceBase:

                    objectiveText.text = "Resource Base";
                    break;
                case ObjectiveType.Forest:

                    objectiveText.text = "Forest";
                    break;
                case ObjectiveType.Mine:

                    objectiveText.text = "The Mines";
                    break;
                case ObjectiveType.Smith:

                    objectiveText.text = "Smith";
                    break;
                case ObjectiveType.GrainField:

                    objectiveText.text = "Grain Field";
                    break;
                case ObjectiveType.Bakery:

                    objectiveText.text = "Bakery";
                    break;
            }
        }
    }

    // The objective has a trigger. If it detects someone entering the trigger it checks to see if it is a worker.
    // If it is a worker it calls TriggerEvents().
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Worker>() == null)
        {
            return;
        }

        Worker worker = other.GetComponent<Worker>();
        TriggerEvents(worker);
    }

    // Here we check what type of objective this is and call the correct function.
    public void TriggerEvents(Worker worker)
    {
        if (type == ObjectiveType.ResourceBase)
        {
            TriggerResourceBase(worker);
        }
        else
        {
            TriggerTask(worker);
        }
    }

    // Since the resource base is like a central hub where workers go to to perform certain actions, this function has a few if statements to check what the worker is up to.
    // Based on that it completes the action the worker is doing.
    private void TriggerResourceBase(Worker worker)
    {
        if (anim != null)
        {
            anim.SetTrigger("Trigger");
        }

        // If the workes is trying to get resources, let him complete that action and start the task hes working on.
        if (worker.action_GetResources.state == BaseAction.State.InProgress)
        {
            worker.action_GetResources.CompleteAction(worker);
            worker.currentTask.StartTask(worker);
            return;
        }

        // If the worker is trying to deposit resources, let him complete that action and give him a new task.
        if (worker.action_DepositResources.state == BaseAction.State.InProgress)
        {
            worker.action_DepositResources.CompleteAction(worker);

            TaskManager.instance.GetNewTask(worker);
            return;
        }
    }

    // This just gets called when the objective isnt the resource base.
    // It calls the DoTask() function on specified worker which basically tells him to start working / gathering since hes already at the correct place to do so.
    private void TriggerTask(Worker worker)
    {
        if (worker.currentTask.state == Task.State.InProgress)
        {
            StartCoroutine(worker.currentTask.DoTask());
        }
    }
}
