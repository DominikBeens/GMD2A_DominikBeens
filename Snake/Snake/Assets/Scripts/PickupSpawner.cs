using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    public static PickupSpawner instance;

    public GameObject spawnedPickup;

    public GameObject pickup;

    public bool canSpawn;

    public float spawnRange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        canSpawn = false;

        Vector3 randomizedSpawn = new Vector3(Random.Range(-spawnRange, spawnRange), 1, Random.Range(-spawnRange, spawnRange));

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(randomizedSpawn.x, randomizedSpawn.y + 4, randomizedSpawn.z), Vector3.down, out hit, 10f))
        {
            if (hit.transform.tag == "Ground")
            {
                spawnedPickup = Instantiate(pickup, randomizedSpawn, Quaternion.identity, GameManager.instance.arena.transform);
            }
            else
            {
                SpawnPickup();
            }
        }
    }
}
