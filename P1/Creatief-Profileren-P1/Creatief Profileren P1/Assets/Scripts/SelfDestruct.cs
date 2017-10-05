using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float destroyAfterTime;

    private void Start()
    {
        Destroy(gameObject, destroyAfterTime);
    }
}
