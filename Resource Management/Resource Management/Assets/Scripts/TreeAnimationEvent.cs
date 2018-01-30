using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimationEvent : MonoBehaviour 
{

    // Simple script that sits on a tree which sets the state of a tree objective to unavailable when a tree is growing and available when it is finished.

    public void TriggerStartAnimation()
    {
        GetComponent<Objective>().availability = Objective.Availability.Unavalable;
    }

    public void TriggerEndAnimation()
    {
        GetComponent<Objective>().availability = Objective.Availability.Available;
    }
}
