using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyMovement : MonoBehaviour
{

    private SnakeMovement snakeMovement;

    public List<RecordTransform> recordedPositions = new List<RecordTransform>();

    private float moveSpeed;

    public Material bodyMat;

    private void Awake()
    {
        snakeMovement = GameObject.FindWithTag("SnakeHead").GetComponent<SnakeMovement>();

        moveSpeed = snakeMovement.moveSpeed;

        bodyMat.color = Random.ColorHSV();
    }

    private void Update()
    {
        if (recordedPositions.Count != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, recordedPositions[0].recordedPosition, (Time.fixedDeltaTime * moveSpeed));

            if (transform.position == recordedPositions[0].recordedPosition)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, recordedPositions[0].recordedYRot, 0));
                recordedPositions.Remove(recordedPositions[0]);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * (Time.fixedDeltaTime * moveSpeed));
        }
    }
}
