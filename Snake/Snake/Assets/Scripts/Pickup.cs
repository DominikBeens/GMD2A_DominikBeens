using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private bool collided;

    public enum PickupType
    {
        Food
    }
    public PickupType pickupType;

    public int points;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SnakeHead" && !collided)
        {
            collided = true;

            GameManager.score += points;
            UIManager.instance.scorePanel.transform.localScale = new Vector3(UIManager.instance.scorePanel.transform.localScale.x + 0.15f, UIManager.instance.scorePanel.transform.localScale.y + 0.15f, 1);

            PickupSpawner.instance.canSpawn = true;

            if (UIManager.instance.difficultyDropdown.value == 3)
            {
                Camera.main.backgroundColor = Random.ColorHSV();
            }

            if (pickupType == PickupType.Food)
            {
                Snake.instance.AddBodyPart();
                Destroy();
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
