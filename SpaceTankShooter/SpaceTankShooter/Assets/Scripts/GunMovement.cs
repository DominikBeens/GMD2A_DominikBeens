using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{

    private Transform player;

    public float rotateSpeed;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        transform.LookAt(GetWorldPositionOnPlane(Input.mousePosition, player.position.z));

        //Quaternion newRotation = Quaternion.LookRotation(GetWorldPositionOnPlane(Input.mousePosition, player.position.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
