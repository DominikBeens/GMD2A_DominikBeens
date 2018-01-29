using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "Bullets/Normal")]
public class Normal : Bullet
{

    public override void Impact(Collision collision, GameObject parent)
    {
        base.Impact(collision, parent);
    }

    public override void ParticleImpact(GameObject other, GameObject parent)
    {
        base.ParticleImpact(other, parent);
    }
}
