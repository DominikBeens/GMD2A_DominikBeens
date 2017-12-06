using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;

    public Spawn_Spawner spawnInfo;

    public List<Transform> spawnPoints = new List<Transform>();

    public float rocketFireForce;

    public Transform targetPlanet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < spawnInfo.spawns.Count; i++)
        {
            for (int ii = 0; ii < spawnInfo.spawns[i].enemies.Count; ii++)
            {
                for (int iii = 0; iii < spawnInfo.spawns[i].enemies[ii].amount; iii++)
                {
                    SpawnEnemy(spawnInfo.spawns[i].enemies[ii].enemy);
                    yield return null;
                }

                if (ii == spawnInfo.spawns[i].enemies.Count - 1)
                {
                    yield return new WaitForSeconds(spawnInfo.spawns[i].endOfWaveCountDown);
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int spawnPoint = Random.Range(0, spawnPoints.Count);

        GameObject enemySpawn = Instantiate(enemy, spawnPoints[spawnPoint].position, Quaternion.identity);

        Enemy enemyComponent = enemySpawn.GetComponent<Enemy>();
        switch (enemyComponent.type)
        {
            case Enemy.Type.Rocket:

                enemySpawn.transform.LookAt(targetPlanet);
                enemySpawn.GetComponent<Rigidbody>().AddForce(enemySpawn.transform.forward * enemyComponent.stats.instantiateForce, ForceMode.Impulse);
                break;
            case Enemy.Type.Charger:

                enemySpawn.transform.LookAt(targetPlanet);
                enemySpawn.GetComponent<Rigidbody>().AddForce(enemySpawn.transform.forward * enemyComponent.stats.instantiateForce, ForceMode.Impulse);
                break;
        }
    }
}
