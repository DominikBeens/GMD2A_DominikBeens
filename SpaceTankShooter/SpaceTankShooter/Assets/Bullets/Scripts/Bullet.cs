using System.Collections;
using UnityEngine;

public class Bullet : ScriptableObject
{

    public enum Type
    {
        Normal
    }
    public Type type;

    public enum Side
    {
        Friendly,
        Enemy
    }
    public Side side;

    public GameObject impactParticle;

    public float damage;

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

    public virtual void Impact(Collision collision, GameObject parent)
    {
        MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        Camera.main.transform.GetComponent<CameraShake>().Shake(0.05f, 3, 3, 3);

        if (side == Side.Friendly)
        {
            if (collision.transform.tag == "Enemy")
            {
                Instantiate(impactParticle, parent.transform.position, Quaternion.identity);

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
                Instantiate(impactParticle, parent.transform.position, Quaternion.identity);

                Planet planet = collision.transform.GetComponent<Planet>();
                if (planet != null)
                {
                    planet.Damage(damage);
                }
            }
        }

        Destroy(parent.gameObject);
    }

    public virtual void ParticleImpact(GameObject other, GameObject parent)
    {
        if (other.tag == "ShieldParticle")
        {
            MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }

            Instantiate(impactParticle, parent.transform.position, Quaternion.identity);

            Destroy(parent.gameObject);
        }
    }
}
