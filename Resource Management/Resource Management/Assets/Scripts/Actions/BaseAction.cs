using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Had to rename this from Action to BaseAction since the post processing stack was interfering with it. :/
public class BaseAction
{
    // Every action has three states. UnCompleted, InProgress and Completed.
    // Actions are like little universal helper functions of tasks.
    // The task itself has all the functionality the worker needs to complete that task but a task can also contain multiple action that need to completed aswell.
    // Right now an action can be to deposit a workers resources or get some resources from the resource base.

    // The Setup() sets the destination of where the action has to be completed.
    // DoAction() basically tells the worker to go to that destination.
    // And CompleteAction() gets called by a trigger on an objective when the actions state is InProgress and holds all the logic.
    public enum State
    {
        UnCompleted,
        InProgress,
        Completed
    }
    public State state;

    public Transform destination;

    [Header("Progress")]
    public float actionTime;
    public float actionProgress;

    public virtual void Setup()
    {
        state = State.UnCompleted;
    }

    public virtual void DoAction(Worker worker)
    {
        state = State.InProgress;
    }

    public virtual void CompleteAction(Worker worker)
    {
        state = State.Completed;
    }
}
