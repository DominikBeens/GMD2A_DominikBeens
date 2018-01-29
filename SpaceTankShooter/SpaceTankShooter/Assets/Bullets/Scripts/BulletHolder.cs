using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{

    public Bullet bullet;

    public void OnCollisionEnter(Collision collision)
    {
        bullet.Impact(collision, gameObject);
    }

    public void OnParticleCollision(GameObject other)
    {
        bullet.ParticleImpact(other, gameObject);
    }
}
