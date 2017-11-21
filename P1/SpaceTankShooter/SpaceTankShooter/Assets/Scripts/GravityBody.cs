using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{

    public GravityAttractor attractor;

    public float rotateSpeed = 25f;

    private void Update()
    {
        attractor.Attract(transform);
    }
}
