using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{

    public AI snakeAI;

    public enum Position
    {
        Front,
        Left,
        Right
    }
    public Position position;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "SnakeBody" || other.tag == "Wall")
        {
            switch (position)
            {
                case Position.Front:
                    snakeAI.collisionFront = true;
                    break;
                case Position.Left:
                    snakeAI.collisionLeft = true;
                    break;
                case Position.Right:
                    snakeAI.collisionRight = true;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SnakeBody" || other.tag == "Wall")
        {
            switch (position)
            {
                case Position.Front:
                    snakeAI.collisionFront = false;
                    break;
                case Position.Left:
                    snakeAI.collisionLeft = false;
                    break;
                case Position.Right:
                    snakeAI.collisionRight = false;
                    break;
            }
        }
    }
}
