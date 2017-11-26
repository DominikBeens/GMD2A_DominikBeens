using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public enum Side
    {
        Friendly,
        Enemy
    }
    public Side side;

    public GameObject impactParticle;

    public float damage;

    public float launchForce;

    public float impactForce;

    public float deactivateTimer;

    private void Awake()
    {
        if (side == Side.Friendly)
        {
            Physics.IgnoreLayerCollision(8, 8, true);
        }
        else
        {
            Physics.IgnoreLayerCollision(9, 9, true);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        Instantiate(impactParticle, transform.position, Quaternion.identity);

        if (side == Side.Friendly)
        {
            if (collision.transform.tag == "Enemy")
            {
                Enemy enemy = collision.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Damage(damage);
                }
            }
        }
        else if (side == Side.Enemy)
        {
            if (collision.transform.tag == "Planet")
            {
                Planet planet = collision.transform.GetComponent<Planet>();
                if (planet != null)
                {
                    planet.Damage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
