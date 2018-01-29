using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner instance;

    public List<Transform> spawners = new List<Transform>();

    public GameObject enemy;

    public bool canSpawn;
    public float spawnTimer;

    public int enemiesToSpawn;
    public List<GameObject> aliveEnemies = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void StartSpawner(int _enemiesToSpawn)
    {
        enemiesToSpawn = _enemiesToSpawn;
        canSpawn = true;
    }

    public void StopSpawner()
    {
        StopAllCoroutines();
        canSpawn = false;

        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in aliveEnemies)
        {
            Destroy(enemy);
        }
    }

    private void Update()
    {
        if (enemiesToSpawn > 0 && canSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        canSpawn = false;

        int spawner =  Random.Range(0, spawners.Count);
        GameObject enemySpawn = Instantiate(enemy, spawners[spawner].position, Quaternion.identity);
        aliveEnemies.Add(enemySpawn);
        enemiesToSpawn--;

        yield return new WaitForSeconds(spawnTimer);

        canSpawn = true;
    }
}
