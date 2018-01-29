using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;

    public Spawn_Spawner spawnInfo;

    public List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> usedSpawnPoints = new List<Transform>();

    public float rocketFireForce;

    public Transform targetPlanet;

    [Header("Wave Text")]
    public TextMeshProUGUI waveText;
    public Animator waveTextAnim;

    [HideInInspector] public int waveCount;

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

    public IEnumerator Spawn()
    {
        waveCount = 1;

        for (int i = 0; i < spawnInfo.spawns.Count; i++)
        {
            ShowWaveText(waveCount);

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

            waveCount++;
            usedSpawnPoints.Clear();

            if (i == spawnInfo.spawns.Count - 1)
            {
                i = 0;
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int spawnPoint = Random.Range(0, spawnPoints.Count);

        GameObject enemySpawn = new GameObject();
        if (!usedSpawnPoints.Contains(spawnPoints[spawnPoint]))
        {
            enemySpawn = Instantiate(enemy, spawnPoints[spawnPoint].position, Quaternion.identity);
            usedSpawnPoints.Add(spawnPoints[spawnPoint]);
        }
        else
        {
            SpawnEnemy(enemy);
            return;
        }

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

    private void ShowWaveText(int wave)
    {
        waveText.text = "Wave " + wave;
        waveTextAnim.SetTrigger("Show");
    }
}
