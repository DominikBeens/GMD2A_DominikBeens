using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public UIManager uim;

    public enum GameState
    {
        Intro,
        Playing,
        Paused
    }
    public GameState gameState;

    public Animator timerAnim;

    [Header("Timer")]
    public float time;

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

        gameState = GameState.Intro;
        uim.introPanel.SetActive(true);
    }

    private void Update()
    {
        if (gameState == GameState.Intro || gameState == GameState.Paused)
        {
            Time.timeScale = 0;

            if (Input.GetButtonDown("Jump"))
            {
                gameState = GameState.Playing;

                if (uim.introPanel.activeInHierarchy)
                {
                    StartCoroutine(StartGame());
                }
                if (uim.pausePanel.activeInHierarchy)
                {
                    uim.pausePanel.SetActive(false);
                }
            }
        }
        else
        {
            Time.timeScale = 1;
            time += Time.deltaTime;
            uim.timerText.text = ((int)(time / 60)).ToString("00") + ":" + ((int)(time % 60)).ToString("00");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = GameState.Paused;
                uim.pausePanel.SetActive(true);
            }
        }
    }

    private IEnumerator StartGame()
    {
        uim.introPanel.SetActive(false);
        timerAnim.SetTrigger("Show");

        yield return new WaitForSeconds(0.5f);

        timerAnim.SetTrigger("Hide");
    }

    public void ReturnToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
