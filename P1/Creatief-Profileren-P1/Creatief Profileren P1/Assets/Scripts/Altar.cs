using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    public static Altar instance;

    public Transform altarObjectPosition;

    public int maxHealth;
    public int currentHealth;

    private bool gameEnd;

    private void Awake()
    {
        instance = this;

        altarObjectPosition = GameObject.FindWithTag("Altar").transform;
    }

    private void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Level)
        {
            UIManager.instance.altarHealth.SetActive(true);
            UIManager.instance.altarHealthFill.fillAmount = (float)currentHealth / maxHealth;

            if (!gameEnd)
            {
                if (currentHealth <= 0)
                {
                    StartCoroutine(GameOver());
                }

                if (Spawner.instance.enemiesToSpawn == 0 && Spawner.instance.aliveEnemies.Count == 0 && currentHealth > 0)
                {
                    StartCoroutine(GameWon());
                }
            }
        }
        else
        {
            UIManager.instance.altarHealth.SetActive(false);
        }
    }

    private IEnumerator GameWon()
    {
        gameEnd = true;

        UIManager.instance.NewNotification("GG EZ", 0f, 2f, 2.25f, 4, Color.white);
        yield return new WaitForSeconds(2f);

        Spawner.instance.StopSpawner();
        StartCoroutine(GameManager.instance.SwitchGameState());

        gameEnd = false;
    }

    private IEnumerator GameOver()
    {
        gameEnd = true;

        UIManager.instance.NewNotification("You", 0f, 2f, 2.25f, 10, Color.white);
        yield return new WaitForSeconds(0.75f);
        UIManager.instance.NewNotification("Lost..", 0f, 2f, 2.25f, 10, Color.white);
        yield return new WaitForSeconds(1.5f);

        Spawner.instance.StopSpawner();
        StartCoroutine(GameManager.instance.SwitchGameState());

        gameEnd = false;
    }
}
