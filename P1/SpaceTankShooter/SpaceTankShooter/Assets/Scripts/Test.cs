using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private Transform player;
    public Vector3 lookAt;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }


    private void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 30;
        //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));

        //transform.position = mousePos;

        //RaycastHit hit;
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //{
        //    if (hit.transform.tag == "Test")
        //    {
        //        lookAt = hit.point;
        //    }
        //}

        //lookAt = GetWorldPositionOnPlane(Input.mousePosition, player.position.z);
        //if (lookAt != null)
        //{
        //    transform.position = lookAt;
        //}
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