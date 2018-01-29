using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager instance;

    [Header("Resources")]
    public Inventory inventory;

    [Header("Bake Costs")]
    public int breadGrainCost;

    [Header("Smith Costs")]
    public int pickaxeOreCost;
    public int pickaxeWoodCost;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
