using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum Type
    {
        Rocket,
        Charger
    }
    public Type type;

    public Stats stats = new Stats();

    public void Damage(float amount)
    {
        stats.currentHealth -= amount;

        if (stats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
