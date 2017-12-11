using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Bullets/Explosive")]
public class Explosive : Bullet
{

    public GameObject impactBullet;
    public float bulletFireForce;

    public int amountToSpawn;

    public override void Impact(Collision collision, GameObject parent)
    {
        base.Impact(collision, parent);

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject bulletInstance = Instantiate(impactBullet, collision.transform.position, Quaternion.Euler(new Vector3(Random.Range(-360, 360), -90, impactBullet.transform.rotation.z)));
            bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletFireForce, ForceMode.Impulse);
        }
    }

    public override void ParticleImpact(GameObject other, GameObject parent)
    {
        base.ParticleImpact(other, parent);
    }
}
