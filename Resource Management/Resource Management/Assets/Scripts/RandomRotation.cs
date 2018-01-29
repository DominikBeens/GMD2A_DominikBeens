using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

}
