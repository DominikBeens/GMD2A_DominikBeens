using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public static Snake instance;

    public List<GameObject> snakeBodyParts = new List<GameObject>();

    public GameObject snakeBodyPart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddBodyPart()
    {
        GameObject bodyPartInstance = Instantiate(snakeBodyPart, transform);

        if (snakeBodyParts.Count == 0)
        {
            bodyPartInstance.transform.localPosition = new Vector3(0, 0, -(snakeBodyParts.Count + 2));
        }
        else
        {
            bodyPartInstance.transform.localPosition = new Vector3(0, 0, -(snakeBodyParts.Count + 2));
        }

        bodyPartInstance.transform.SetParent(null);
        snakeBodyParts.Add(bodyPartInstance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SnakeBody" || other.tag == "Wall")
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;

            UIManager.instance.scorePanel.SetActive(false);
            UIManager.instance.deathPanel.SetActive(true);
            UIManager.instance.endScoreText.text = "Final Score: " + GameManager.score;
        }
    }
}
