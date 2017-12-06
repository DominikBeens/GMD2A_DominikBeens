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

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //switch (type)
        //{
        //    case Type.Rocket:
        //        break;
        //    case Type.Charger:

        //        if (Vector3.Distance(SpawnManager.instance.targetPlanet.position, transform.position) < 30)
        //        {
        //            rb.velocity = Vector3.zero;
        //            rb.mass = 100;

        //            if (gameObject.GetComponent<GravityAttractor>() == null)
        //            {
        //                rb.AddForce(Vector3.right * stats.instantiateForce * 75, ForceMode.Impulse);
        //                gameObject.AddComponent<GravityAttractor>();
        //            }
        //        }
        //        break;
        //}
    }

    public void Damage(float amount)
    {
        stats.currentHealth -= amount;

        if (stats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
