using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class Dash : Ability
{

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;

    public ParticleSystem dashTrailParticle;

    private PlayerMovement playerMovement;

    public override void Initialize()
    {
        playerMovement = PlayerMovement.instance;
        dashTrailParticle = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponent<ParticleSystem>();

        Use();
    }

    public override void Use()
    {
        AbilityManager.instance.StartACoroutine(DashRoutine(dashTime));
    }

    public IEnumerator DashRoutine(float time)
    {
        dashTrailParticle.Play();

        float curSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed = dashSpeed;

        yield return new WaitForSeconds(time);

        playerMovement.moveSpeed = curSpeed;
    }
}
