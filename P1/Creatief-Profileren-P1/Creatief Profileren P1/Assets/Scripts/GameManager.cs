using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private Transform player;

    public Transform shopSpawn;
    public Transform levelSpawn;

    public enum GameState
    {
        Shopping,
        Level
    }
    public GameState gameState;

    private void Awake()
    {
        instance = this;

        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(SwitchGameState());
        }
    }

    private IEnumerator StartGame()
    {
        gameState = GameState.Shopping;
        player.transform.position = shopSpawn.position;

        UIManager.instance.NewNotification("Welcome", 0f, 2f, 2.25f, 10, Color.white);
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.NewNotification("Pick", 0f, 2f, 2.25f, 10, Color.white);
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.NewNotification("A", 0f, 2f, 2.25f, 10, Color.white);
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.NewNotification("Weapon", 0f, 2f, 2.25f, 10, Color.white);
    }

    public IEnumerator SwitchGameState()
    {
        if (gameState == GameState.Shopping)
        {
            Altar.instance.currentHealth = Altar.instance.maxHealth;

            Spawner.instance.StartSpawner(10);

            gameState = GameState.Level;
            player.transform.position = levelSpawn.position;

            int i = Random.Range(0, 3);
            if (i == 0)
            {
                UIManager.instance.NewNotification("Kill", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("All", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("Survive", 0f, 2f, 2.25f, 10, Color.white);
            }
            else if (i == 1)
            {
                UIManager.instance.NewNotification("Defend", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("The", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("Altar", 0f, 2f, 2.25f, 10, Color.white);
            }
            else if (i == 2)
            {
                UIManager.instance.NewNotification("Altar", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("Can't", 0f, 2f, 2.25f, 10, Color.white);
                yield return new WaitForSeconds(0.75f);
                UIManager.instance.NewNotification("Die", 0f, 2f, 2.25f, 10, Color.white);
            }
        }
        else if (gameState == GameState.Level)
        {
            gameState = GameState.Shopping;
            player.transform.position = shopSpawn.position;

            UIManager.instance.NewNotification("Time", 0f, 2f, 2.25f, 10, Color.white);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.NewNotification("To", 0f, 2f, 2.25f, 10, Color.white);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.NewNotification("Shop", 0f, 2f, 2.25f, 10, Color.white);
        }
    }
}
