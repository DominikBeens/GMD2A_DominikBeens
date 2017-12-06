using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{

    public Ability ability;

    public Image cooldownFillImage;

    [HideInInspector] public float cooldown;
    [HideInInspector] public float cooldownFill;
    [HideInInspector] public float cooldownAmount;

    private void Update()
    {
        TeleportCooldownToUI();

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= cooldown)
        {
            cooldown = Time.time + 1f / ability.timer;
            cooldownAmount = cooldown - Time.time;

            cooldownFill = 0;

            ability.Initialize();
        }
    }

    private void TeleportCooldownToUI()
    {
        if (cooldownFill < 1)
        {
            cooldownFill += 1 / cooldownAmount * Time.deltaTime;
            cooldownFillImage.fillAmount = cooldownFill;
        }
        else
        {
            cooldownFill = 1;
        }
    }
}
