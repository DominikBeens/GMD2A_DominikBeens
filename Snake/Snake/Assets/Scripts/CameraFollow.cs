using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform snake;

    private Vector3 startOffset;
    private Vector3 currentOffset;

    public float followSpeed;

    private void Awake()
    {
        snake = GameObject.FindWithTag("SnakeHead").transform;

        startOffset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, snake.transform.position + startOffset, followSpeed * Time.deltaTime);
    }
}
