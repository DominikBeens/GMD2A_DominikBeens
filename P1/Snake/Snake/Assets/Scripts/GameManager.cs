using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public SnakeMovement snakeMovement;

    public GameObject arena;

    public static int score;

    public int casualMovespeed;
    public int kidMovespeed;
    public int hardcoreKoreanMovespeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PickupSpawner.instance.canSpawn = true;
    }

    private void Update()
    {
        if (UIManager.instance.difficultyDropdown.value == 3)
        {
            arena.transform.RotateAround(arena.transform.position, Vector3.up, Time.deltaTime * (1 + (score * 0.1f)));
        }
    }

    public void PlayButton()
    {
        score = 0;

        arena.transform.rotation = Quaternion.Euler(Vector3.zero);

        if (Snake.instance.snakeBodyParts.Count > 0)
        {
            foreach (GameObject snakeBodyPart in Snake.instance.snakeBodyParts)
            {
                Destroy(snakeBodyPart);
            }
        }
        Snake.instance.snakeBodyParts = new List<GameObject>();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        Snake.instance.transform.position = new Vector3(0, 1, 0);
        Snake.instance.transform.rotation = Quaternion.Euler(Vector3.zero);

        UIManager.instance.introPanel.SetActive(false);
        UIManager.instance.deathPanel.SetActive(false);
        UIManager.instance.scorePanel.SetActive(true);

        switch (UIManager.instance.difficultyDropdown.value)
        {
            case 0:
                snakeMovement.moveSpeed = casualMovespeed;
                break;
            case 1:
                snakeMovement.moveSpeed = kidMovespeed;
                break;
            case 2:
                snakeMovement.moveSpeed = hardcoreKoreanMovespeed;
                break;
            case 3:
                snakeMovement.moveSpeed = hardcoreKoreanMovespeed;
                break;
        }
    }
}
