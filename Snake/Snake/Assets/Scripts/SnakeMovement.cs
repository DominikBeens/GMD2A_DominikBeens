using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    public float moveSpeed;

    private void Update()
    {
        InputChecks();

        transform.Translate(Vector3.forward * (Time.fixedDeltaTime * moveSpeed));
    }

    private void InputChecks()
    {
        if (Input.GetButtonDown("W"))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            RecordPosition();
        }
        if (Input.GetButtonDown("A"))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
            RecordPosition();
        }
        if (Input.GetButtonDown("S"))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            RecordPosition();
        }
        if (Input.GetButtonDown("D"))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            RecordPosition();
        }
    }

    public void RecordPosition()
    {
        RecordTransform recording = new RecordTransform()
        {
            recordedPosition = transform.position,
            recordedYRot = transform.eulerAngles.y
        };

        foreach (GameObject snakeBodyPart in Snake.instance.snakeBodyParts)
        {
            snakeBodyPart.GetComponent<SnakeBodyMovement>().recordedPositions.Add(recording);
        }
    }
}
