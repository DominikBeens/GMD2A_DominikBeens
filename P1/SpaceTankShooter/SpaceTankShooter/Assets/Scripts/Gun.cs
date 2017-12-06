using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform bulletSpawn;

    public enum FireMode
    {
        Single,
        Auto,
        Charge
    }
    [Header("Gun Properties")]
    public FireMode fireMode;
    [Space(10)]
    public GameObject bullet;
    public GameObject bullet2;

    public float bulletFireForce;

    public GameObject shootParticle;
    
    [Header("Left Mouse Button")]
    public float leftMBFireRate;
    private float leftMBFireRateCooldown;
    private float leftMBFill;
    private float leftMBFillCooldown;

    [Header("Right Mouse Button")]
    public float rightMBFireRate;
    private float rightMBFireRateCooldown;
    private float rightMBFill;
    private float rightMBFillCooldown;

    [Space(10)]
    public int maxCharge;
    public int currentCharge;
    [Space(10)]
    public float reloadStep;
    private bool reloading;

    [Header("Camera Shake")]
    public CameraShake camShake;
    public float shakeDurationPerShot;
    public float shakeAmountPerShotXY;
    public float shakeAmountPerShotZ;
    public float rotateAmountPerShot;
    public float rightMouseButtonShakeMultiplier;

    private void Update()
    {
        FireCooldownToUI();

        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            if (currentCharge > 0 && !reloading)
            {
                if (fireMode == FireMode.Auto)
                {
                    if (Input.GetButton("Fire1") && Time.time >= leftMBFireRateCooldown)
                    {
                        leftMBFireRateCooldown = Time.time + 1f / leftMBFireRate;
                        leftMBFillCooldown = leftMBFireRateCooldown - Time.time;
                        ShootLeftMB();
                        camShake.Shake(shakeDurationPerShot, shakeAmountPerShotXY, shakeAmountPerShotZ, rotateAmountPerShot);
                    }
                }
                else if (fireMode == FireMode.Single)
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= leftMBFireRateCooldown)
                    {
                        leftMBFireRateCooldown = Time.time + 1f / leftMBFireRate;
                        leftMBFillCooldown = leftMBFireRateCooldown - Time.time;
                        ShootLeftMB();
                        camShake.Shake(shakeDurationPerShot, shakeAmountPerShotXY, shakeAmountPerShotZ, rotateAmountPerShot);
                    }
                }

                if (Input.GetButtonDown("Fire2") && Time.time >= rightMBFireRateCooldown)
                {
                    rightMBFireRateCooldown = Time.time + 1f / rightMBFireRate;
                    rightMBFillCooldown = rightMBFireRateCooldown - Time.time;
                    ShootRightMB();
                    camShake.Shake(shakeDurationPerShot, shakeAmountPerShotXY * rightMouseButtonShakeMultiplier, shakeAmountPerShotZ * rightMouseButtonShakeMultiplier, rotateAmountPerShot * rightMouseButtonShakeMultiplier);
                }

                if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
                {
                    shootParticle.SetActive(true);
                }
                else
                {
                    shootParticle.SetActive(false);
                }
            }
            else
            {
                if (!reloading)
                {
                    StartCoroutine(Reload());
                    shootParticle.SetActive(false);
                }
            }
        }
    }

    private void ShootLeftMB()
    {
        currentCharge--;
        leftMBFill = 0;

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.transform.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletFireForce, ForceMode.Impulse);
    }

    private void ShootRightMB()
    {
        currentCharge--;
        rightMBFill = 0;

        GameObject bulletInstance = Instantiate(bullet2, bulletSpawn.position, bulletSpawn.transform.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletFireForce, ForceMode.Impulse);
    }

    private IEnumerator Reload()
    {
        reloading = true;

        float load = 0;
        float maxload = maxCharge;

        while (load < maxload)
        {
            load += reloadStep;
            currentCharge = (int)load;
            yield return null;
        }

        reloading = false;
    }

    private void FireCooldownToUI()
    {
        if (!reloading)
        {
            if (leftMBFill < 1)
            {
                leftMBFill += 1 / leftMBFillCooldown * Time.deltaTime;
                UIManager.instance.leftMouseButtonCooldown.fillAmount = leftMBFill;
            }
            else
            {
                leftMBFill = 1;
            }

            if (rightMBFill < 1)
            {
                // backwards (rightMBFireRateCooldown - Time.time) * 0.2f;
                rightMBFill += 1 / rightMBFillCooldown * Time.deltaTime;
                UIManager.instance.rightMouseButtonCooldown.fillAmount = rightMBFill;
            }
            else
            {
                rightMBFill = 1;
            }
        }
        else
        {
            UIManager.instance.leftMouseButtonCooldown.fillAmount = ((float)currentCharge / maxCharge);
            UIManager.instance.rightMouseButtonCooldown.fillAmount = ((float)currentCharge / maxCharge);
        }

        UIManager.instance.weaponCharge.fillAmount = ((float)currentCharge / maxCharge);
    }
}
