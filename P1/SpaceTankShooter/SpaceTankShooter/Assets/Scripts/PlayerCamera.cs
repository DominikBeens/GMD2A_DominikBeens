using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private Transform player;

    public Vector3 offset;

    public float rotateSpeed;
    public float moveSpeed;

    //private Vector3 transPos;

    //public float maxCameraLerpDistance;
    //private float cameraStartPosX;
    //private float cameraStartPosY;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        //cameraStartPosX = transform.position.x;
        //cameraStartPosY = transform.position.y;
    }

    private void Update()
    {
        //transPos = new Vector3(Mathf.Clamp(transform.position.x, cameraStartPosX - maxCameraLerpDistance, cameraStartPosX + maxCameraLerpDistance), 
        //                       Mathf.Clamp(transform.position.y, cameraStartPosY - maxCameraLerpDistance, cameraStartPosY + maxCameraLerpDistance),
        //                       transform.position.z);
    }

    private void LateUpdate()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);

        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * moveSpeed);
    }
}
