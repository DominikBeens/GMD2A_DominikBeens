using UnityEngine;

[System.Serializable]
public class Stats
{

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    [Space(10)]
    public float moveSpeed;
    [Space(10)]
    public float instantiateForce;
    [Space(10)]

    [Header("Attack")]
    public float damage;
    public float attackForce;
}
