using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Score")]
    public GameObject scorePanel;
    private float scorePanelScale;
    public Text scoreText;

    [Header("Intro")]
    public GameObject introPanel;
    public Dropdown difficultyDropdown;

    [Header("Death")]
    public GameObject deathPanel;
    public Text endScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        scorePanelScale = scorePanel.transform.localScale.x;
    }

    private void Update()
    {
        scoreText.text = GameManager.score.ToString();

        scorePanel.transform.localScale = Vector3.Lerp(scorePanel.transform.localScale, new Vector3(scorePanelScale, scorePanelScale, 1), (Time.deltaTime * 10));
    }
}
