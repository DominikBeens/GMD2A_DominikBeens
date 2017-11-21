using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject impactParticle;

    public float damage;

    public float impactForce;

    public float deactivateTimer;

    private void Awake()
    {
        StartCoroutine(DeactivateTimer());
    }

    public void OnCollisionEnter(Collision collision)
    {
        impactParticle.SetActive(true);

        MeshRenderer renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        if (collision.transform.tag == "Enemy")
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }
        }

        if (collision.transform.tag == "Planet")
        {
            Planet planet = collision.transform.GetComponent<Planet>();
            if (planet != null)
            {
                planet.Damage(damage);
            }
        }
    }

    private IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(deactivateTimer);
        Destroy(gameObject);
    }
}
