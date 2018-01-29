using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{

    public Node headNode;

    public int numberToCheck;
    [Space(10)]
    public int startingNumber;
    public int maxTreeLength;

    public void CreateTree()
    {
        headNode = new Node()
        {
            number = 5,

            node1 = new Node()
            {
                number = 3,

                node1 = new Node()
                {
                    number = 1
                },
                node2 = new Node()
                {
                    number = 4
                }
            },
            node2 = new Node()
            {
                number = 7,

                node1 = new Node()
                {
                    number = 6
                },
                node2 = new Node()
                {
                    number = 10
                }
            }
        };
    }

    public void CreateTree2()
    {
        if (startingNumber > 0)
        {
            headNode = new Node()
            {
                number = startingNumber
            };

            if (startingNumber - 2 >= 0)
            {
                headNode.node1 = new Node()
                {
                    number = startingNumber - 2
                };
                headNode.node2 = new Node()
                {
                    number = startingNumber + 2
                };
            }
        }
        else
        {
            Debug.Log("Can't create a tree with a negative starting number!");
        }
    }

    public bool IsInTree(int i)
    {
        if (i == headNode.number)
        {
            Debug.Log("Yup it's in there");
            return true;
        }
        else
        {
            if (i < headNode.number)
            {
                if (headNode.node1 != null)
                {
                    headNode.node1.IsInTree(i);
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
                if (headNode.node2 != null)
                {
                    headNode.node2.IsInTree(i);
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

    public void Reset()
    {
        headNode = null;
        numberToCheck = 0;
    }
}
