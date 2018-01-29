using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/Teleport")]
public class Teleport : Ability
{

    [Header("Init")]
    public GameObject planet;
    public Transform player;

    public float teleportResizeStep = 0.1f;
    public float teleportResizeWaitStep = 0.05f;
    public GameObject particle;

    public override void Initialize()
    {
        base.Initialize();

        planet = GameObject.FindWithTag("Planet");
        player = GameObject.FindWithTag("Player").transform;

        Use();
    }

    public override void Use()
    {
        state = AbilityState.Using;
        AbilityManager.instance.StartACoroutine(TeleportRoutine());
    }

    public IEnumerator TeleportRoutine()
    {
        while (player.localScale.x > 0.1f)
        {
            player.localScale = new Vector3(player.localScale.x - teleportResizeStep, player.localScale.y - teleportResizeStep, player.localScale.z - teleportResizeStep);
            yield return null;
        }
        Instantiate(particle, player.position, Quaternion.identity);

        player.position += -player.up * planet.GetComponent<Renderer>().bounds.size.z * 2;
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
        yield return null;

        Instantiate(particle, player.position, Quaternion.identity);
        while (player.localScale.x < 1f)
        {
            player.localScale = new Vector3(player.localScale.x + teleportResizeStep, player.localScale.y + teleportResizeStep, player.localScale.z + teleportResizeStep);
            yield return null;
        }
    }
}
