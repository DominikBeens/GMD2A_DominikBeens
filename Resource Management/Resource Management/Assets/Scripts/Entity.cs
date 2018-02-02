using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{

    [HideInInspector]
    public NavMeshAgent agent;

    // This gets displayed on a panel when you click on an entity.
    [HideInInspector]
    public string currentJobDescription;

    // Contains an arrow above the entity.
    // This gets set active above the worker you clicked on.
    public GameObject selectedObject; 

    [Header("Inventory")]
    public Inventory inventory;

    [Header("Stats")]
    public Stats stats;
    // Keeps track of when this dudes stats need to be checked and updated again.
    private float statCheckCooldown;

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        // Stat depletion runs on a timer.
        if (Time.time >= statCheckCooldown)
        {
            statCheckCooldown = Time.time + stats.statUpdateRate;
            stats.StatDepletion();

            // Check if health is less than zero.
            // Would like to have this check in Stats itself but i cant destroy from there.
            if (stats.health.currentAmount <= 0)
            {
                Debug.LogWarning("Entity died!");
                Destroy(gameObject);
            }
        }
    }
}
