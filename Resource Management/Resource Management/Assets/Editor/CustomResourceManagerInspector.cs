using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourceManager))]
public class CustomResourceManagerInspector : Editor 
{

    public ResourceManager rm;

    private void OnEnable()
    {
        rm = (ResourceManager)target;
    }

    public override void OnInspectorGUI()
    {
        #region Inventory
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Inventory", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            AddItem();
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < rm.inventory.items.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Item", EditorStyles.boldLabel);

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                RemoveItem(i);
                return;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            rm.inventory.items[i].itemName = EditorGUILayout.TextField("Name", rm.inventory.items[i].itemName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            rm.inventory.items[i].quantity = EditorGUILayout.IntField("Quantity", rm.inventory.items[i].quantity);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }
        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        #region Bake Costs

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bake Costs", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bread", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        rm.breadGrainCost = EditorGUILayout.IntField("Grain", rm.breadGrainCost);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        #endregion

        EditorGUILayout.Space();

        #region Smith Costs
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Smith Costs", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Pickaxe", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        rm.pickaxeWoodCost = EditorGUILayout.IntField("Wood", rm.pickaxeWoodCost);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        rm.pickaxeOreCost = EditorGUILayout.IntField("Ore", rm.pickaxeOreCost);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        #endregion

    }

    private void AddItem()
    {
        rm.inventory.items.Add(new Item("", 0));
    }

    private void RemoveItem(int i)
    {
        rm.inventory.items.RemoveAt(i);
    }
}
