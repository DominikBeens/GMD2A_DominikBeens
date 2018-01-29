using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PlanetShield")]
public class PlanetShield : Ability
{

    [Header("PlanetShield")]
    public GameObject worldShieldParticle;

    public override void Initialize()
    {
        Use();
    }

    public override void Use()
    {
        Shield();
    }

    private void Shield()
    {
        Instantiate(worldShieldParticle, Vector3.zero, Quaternion.identity);
    }
}
