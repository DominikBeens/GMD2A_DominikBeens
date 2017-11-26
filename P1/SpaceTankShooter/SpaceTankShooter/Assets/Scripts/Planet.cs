using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{

    public Text healthText;

    public float maxHealth;
    public float currentHealth;

    private void Update()
    {
        healthText.text = (currentHealth / maxHealth * 100).ToString() + "%";
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            print("Planet destroyed");
        }
    }
}
