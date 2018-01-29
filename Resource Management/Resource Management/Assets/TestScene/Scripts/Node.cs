using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{

    public int number;

    public Node node1;
    public Node node2;

    public bool IsInTree(int i)
    {
        if (i == number)
        {
            Debug.Log("Yup it's in there");
            return true;
        }
        else
        {
            if (i < number)
            {
                if (node1 != null)
                {
                    node1.IsInTree(i);
                    return false;
                }
                else
                {
                    Debug.Log("Nope, not in here");
                    return false;
                }
            }
            else
            {
                if (node2 != null)
                {
                    node2.IsInTree(i);
                    return false;
                }
                else
                {
                    Debug.Log("Nope, not in here");
                    return false;
                }
            }
        }
    }
}
