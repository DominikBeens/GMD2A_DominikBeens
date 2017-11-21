using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private int waveCount;

    public List<Transform> spawnPoints = new List<Transform>();

    public List<GameObject> rockets = new List<GameObject>();
    public float rocketFireForce;

    public Transform targetPlanet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnRocket();
        }
    }

    private void SpawnRocket()
    {
        GameObject rocketSpawn = Instantiate(rockets[Random.Range(0, rockets.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);

        rocketSpawn.transform.LookAt(targetPlanet);

        rocketSpawn.GetComponent<Rigidbody>().AddForce(rocketSpawn.transform.forward * rocketFireForce, ForceMode.Impulse);
    }
}
