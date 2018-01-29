using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{

    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public string currentJobDescription;

    // Contains an arrow above the entity.
    // This gets set active above the worker you clicked on.
    public GameObject selectedObject; 

    [Header("Inventory")]
    public Inventory inventory;

    [Header("Stats")]
    public Stats stats;
    private float statDepletionCooldown;

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        // Stat depletion runs on a timer.
        if (Time.time >= statDepletionCooldown)
        {
            statDepletionCooldown = Time.time + stats.statUpdateRate;
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
