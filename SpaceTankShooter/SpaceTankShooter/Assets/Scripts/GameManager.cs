using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public UIManager uim;

    public enum GameState
    {
        Intro,
        Playing,
        Paused,
        End
    }
    public GameState gameState;

    public Animator timerAnim;
    public Animator weaponInfoAnim;

    public PostProcessingBehaviour pPBehaviour;

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
            pPBehaviour.enabled = false;
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
        else if (gameState == GameState.Playing)
        {
            pPBehaviour.enabled = true;
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

        uim.weaponInfoPanel.SetActive(true);
        //weaponInfoAnim.SetTrigger("Show");

        yield return new WaitForSeconds(weaponInfoAnim.GetCurrentAnimatorClipInfo(0).Length);

        uim.timerPanel.SetActive(true);
        timerAnim.SetTrigger("Show");

        yield return new WaitForSeconds(0.5f);

        timerAnim.SetTrigger("Hide");

        yield return new WaitForSeconds(3.5f);

        SpawnManager.instance.StartCoroutine(SpawnManager.instance.Spawn());
    }

    public IEnumerator EndGame()
    {
        while (Time.timeScale > 0.2f)
        {
            Time.timeScale -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        Time.timeScale = 0;

        SpawnManager.instance.waveText.gameObject.SetActive(false);
        uim.wavesDefendedText.text = "Waves Defended: " + (SpawnManager.instance.waveCount - 1);
        uim.levelEndPanel.SetActive(true);
    }

    public void ReturnToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
