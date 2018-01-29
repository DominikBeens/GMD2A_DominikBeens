using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Entity), true)]
public class CustomWorkerInspector : Editor 
{

    public Worker entity;

    private bool showInventory;
    private bool showStats;

    private void OnEnable()
    {
        entity = (Worker)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        #region Worker State
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Worker Current State", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        entity.state = (Worker.State)EditorGUILayout.EnumPopup(entity.state);
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.Space();

        #region Worker Current Task
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Worker Current Task", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(entity.currentTask.GetType().Name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        entity.currentTask.state = (Task.State)EditorGUILayout.EnumPopup(entity.currentTask.state);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        entity.currentTask.minimumWorkTime = EditorGUILayout.FloatField("Minimum Work Time", entity.currentTask.minimumWorkTime);
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.Space();

        #region UI Stuff
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("UI Stuff", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.ObjectField("Selected indicator", entity.selectedObject, typeof(GameObject), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedObject"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("workProgressObject"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("workProgressFill"));
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.Space();

        #region Inventory
        EditorGUILayout.BeginHorizontal();
        showInventory = EditorGUILayout.Foldout(showInventory, "Inventory", EditorStyles.foldout);
        EditorGUILayout.EndHorizontal();
        if (showInventory)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                AddItem();
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < entity.inventory.items.Count; i++)
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
                entity.inventory.items[i].itemName = EditorGUILayout.TextField("Name", entity.inventory.items[i].itemName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                entity.inventory.items[i].quantity = EditorGUILayout.IntField("Quantity", entity.inventory.items[i].quantity);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
            }
        }
        #endregion

        EditorGUILayout.Space();

        #region Stats
        EditorGUILayout.BeginHorizontal();
        showStats = EditorGUILayout.Foldout(showStats, "Stats", EditorStyles.foldout);
        EditorGUILayout.EndHorizontal();
        if (showStats)
        {
            #region Health
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            entity.stats.health.maxAmount = EditorGUILayout.FloatField("Max Health", entity.stats.health.maxAmount);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            entity.stats.health.currentAmount = EditorGUILayout.FloatField("Current Health", entity.stats.health.currentAmount);
            if (GUILayout.Button("Fill", GUILayout.Width(40)))
            {
                FillHealth();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            entity.stats.health.gainRate = EditorGUILayout.FloatField("Gain Rate", entity.stats.health.gainRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            entity.stats.health.drainRate = EditorGUILayout.FloatField("Drain Rate", entity.stats.health.drainRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            #endregion

            EditorGUILayout.Space();

            #region Hunger
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Hunger", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            entity.stats.hunger.maxAmount = EditorGUILayout.FloatField("Max Hunger", entity.stats.hunger.maxAmount);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            entity.stats.hunger.currentAmount = EditorGUILayout.FloatField("Current Hunger", entity.stats.hunger.currentAmount);
            if (GUILayout.Button("Fill", GUILayout.Width(40)))
            {
                FillHunger();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            entity.stats.hunger.gainRate = EditorGUILayout.FloatField("Gain Rate", entity.stats.hunger.gainRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            entity.stats.hunger.drainRate = EditorGUILayout.FloatField("Drain Rate", entity.stats.hunger.drainRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            #endregion
        }
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    private void AddItem()
    {
        entity.inventory.items.Add(new Item("", 0));
    }

    private void RemoveItem(int i)
    {
        entity.inventory.items.RemoveAt(i);
    }

    private void FillHealth()
    {
        entity.stats.health.currentAmount = entity.stats.health.maxAmount;
    }

    private void FillHunger()
    {
        entity.stats.hunger.currentAmount = entity.stats.hunger.maxAmount;
    }
}
