using UnityEngine;

public class RandomRotation : MonoBehaviour
{

    // Simple script that sits on the trees to give them a random y-rotation when the game starts.

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
