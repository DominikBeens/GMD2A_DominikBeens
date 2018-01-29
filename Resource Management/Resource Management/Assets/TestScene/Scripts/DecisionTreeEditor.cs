using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DecisionTree))]
public class DecisionTreeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DecisionTree decisionTree = (DecisionTree)target;
        if (GUILayout.Button("Create Tree"))
        {
            decisionTree.CreateTree();
        }
        if (GUILayout.Button("Check Number"))
        {
            decisionTree.IsInTree(decisionTree.numberToCheck);
        }
        if (GUILayout.Button("Reset"))
        {
            decisionTree.Reset();
        }
    }
}
