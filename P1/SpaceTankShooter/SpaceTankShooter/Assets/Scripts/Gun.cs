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
    public int maxAmmo;
    public int currentAmmo;
    public int maxClipAmmo;
    public int currentClipAmmo;
    [Space(10)]
    public float reloadTime;
    private float reloadCooldownFill;
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

        if (currentAmmo > 0)
        {
            if (currentClipAmmo > 0)
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

            }
            else
            {
                if (!reloading)
                {
                    StartCoroutine(Reload(reloadTime));
                }
            }
        }
    }

    private void ShootLeftMB()
    {
        currentClipAmmo--;
        leftMBFill = 0;

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.transform.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletInstance.GetComponent<Bullet>().launchForce, ForceMode.Impulse);
    }

    private void ShootRightMB()
    {
        currentClipAmmo--;
        rightMBFill = 0;

        GameObject bulletInstance = Instantiate(bullet2, bulletSpawn.position, bulletSpawn.transform.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletInstance.GetComponent<Bullet>().launchForce, ForceMode.Impulse);
    }

    private IEnumerator Reload(float reloadTime)
    {
        reloading = true;
        reloadCooldownFill = 0;

        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = maxClipAmmo - currentClipAmmo;

        currentAmmo -= ammoNeeded;
        currentClipAmmo += ammoNeeded;

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
            reloadCooldownFill += 1 / reloadTime * Time.deltaTime;
            UIManager.instance.leftMouseButtonCooldown.fillAmount = reloadCooldownFill;
            UIManager.instance.rightMouseButtonCooldown.fillAmount = reloadCooldownFill;
        }
    }
}
