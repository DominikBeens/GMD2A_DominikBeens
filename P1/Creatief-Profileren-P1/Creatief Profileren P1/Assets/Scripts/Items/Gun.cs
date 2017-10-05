using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon
{

    private Transform mainCam;
    private Transform mainCamHolder;

    private Vector3 movementLerpOffset;
    private Transform movementLookAtObject;

    private GunMovement gunMovement;

    private Transform weaponHolder;
    private Vector3 weaponHolderDefaultPos;

    private PlayerController playerController;
    private float defaultMouseSens;

    private bool canRecoilDown = true;

    public enum FireMode
    {
        Single,
        Auto,
        Charge
    }
    [Header("Gun Properties")]
    public FireMode fireMode;
    [Space(10)]
    public float damage;
    public float fireDistance;
    public float fireRate;
    private float fireRateCooldown;
    [Space(10)]
    public int maxAmmo;
    public int currentAmmo;
    public int maxClipAmmo;
    public int currentClipAmmo;
    [Space(10)]
    public float reloadTime;
    private float reloadCooldownFillAmount;
    private bool reloading;
    [Space(10)]
    public GameObject bulletHitEffect;
    [Space(10)]
    public float bulletHitForce;
    [Space(10)]
    public float fovKick;
    public float fovFallbackSpeed;
    private float defaultFov;
    private Camera mainCamComponent;

    public enum ScreenRecoilMode
    {
        Up,
        Left,
        Right,
        UpLeft,
        UpRight,
        RandomUp,
        RandomLeft,
        RandomRight,
        RandomUpLeftRight,
        None
    }
    [Header("Recoil Options")]
    public ScreenRecoilMode screenRecoilMode;
    [Space(10)]
    public float verticalScreenRecoilDistance;
    public float horizontalScreenRecoilDistance;
    private float currentScreenRecoil;
    [Space(10)]
    public float horizontalGunRecoilDistance;
    public float horizontalGunRecoilSpeed;
    private float currentGunRecoil;
    [Space(10)]
    public float recoilSpeed;
    public float recoilFallBackSpeed;
    public float currentRecoilFallBackSpeed;

    [Header("Bullet Spray Options")]
    public float horizontalSpray;
    public float verticalSpray;
    [Space(10)]
    public float sprayPerShotIncreaseAmount;
    private Vector3 lastSpray;

    [Header("Gun Movement")]
    public float swayHorizontal;
    public float swayVerticalA;
    public float swayVerticalB;
    [Space(10)]
    public float verticalBumpOnFire;
    public float verticalBumpSpeed;
    private bool lookAtGunMovement = true;

    [Header("Scope Options")]
    public GameObject scopeCamera;
    public Vector3 lerpToBeforeScope;
    private bool scoped;
    [Space(10)]
    public float scopeSpeed;
    public float scopedMouseSensMultiplier;
    private float scopedDamageFillAmount;

    [Header("Charged Shooting Options")]
    public float maxChargedDamage;
    public float chargeSpeed;
    private float currentChargeDamage;

    [Header("UI")]
    public Color fireRateCrosshairColor;
    public Color reloadCooldownCrosshairColor;
    private Image fireRateFill;
    private float fireRateCooldownFillAmount;
    [Space(10)]
    public float ammoTextScaleMultiplier;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        mainCamHolder = mainCam.parent;

        currentAmmo = maxAmmo;
        currentClipAmmo = maxClipAmmo;

        movementLerpOffset = transform.localPosition;
        movementLookAtObject = GameObject.FindWithTag("GunMovement").transform;

        gunMovement = GameObject.FindWithTag("GunMovement").GetComponent<GunMovement>();

        weaponHolder = GameObject.FindWithTag("WeaponHolder").transform;
        weaponHolderDefaultPos = weaponHolder.localPosition;

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        defaultMouseSens = playerController.lookSensitivity;

        currentChargeDamage = damage;

        mainCamComponent = mainCam.gameObject.GetComponent<Camera>();
        defaultFov = mainCamComponent.fieldOfView;

        fireRateFill = UIManager.instance.fireRateFill;
    }

    private void Update()
    {
        Sway();
        FireRateToUI();
        Scope();
        FOV();

        if (UIManager.instance.uiState == UIManager.UIState.CannotInteractWithUI)
        {
            if (currentAmmo > 0)
            {
                if (currentClipAmmo > 0)
                {
                    if (fireMode == FireMode.Auto)
                    {
                        if (Input.GetButton("Fire1") && Time.time >= fireRateCooldown)
                        {
                            fireRateCooldown = Time.time + 1f / fireRate;
                            canRecoilDown = false;
                            Shoot(damage);
                        }
                        if (Input.GetButtonUp("Fire1"))
                        {
                            canRecoilDown = true;

                            if (sprayPerShotIncreaseAmount > 0)
                            {
                                lastSpray = Vector3.zero;
                            }
                        }
                    }
                    else if (fireMode == FireMode.Single)
                    {
                        if (Input.GetButtonDown("Fire1") && Time.time >= fireRateCooldown)
                        {
                            fireRateCooldown = Time.time + 1f / fireRate;
                            Shoot(damage);
                        }
                    }
                    else if (fireMode == FireMode.Charge)
                    {
                        if (Input.GetButton("Fire2"))
                        {
                            if (currentChargeDamage < maxChargedDamage)
                            {
                                currentChargeDamage += Time.deltaTime * chargeSpeed;

                                if (maxChargedDamage - currentChargeDamage <= 0.1f)
                                {
                                    currentChargeDamage = maxChargedDamage;
                                }
                            }
                        }
                        if (Input.GetButtonDown("Fire1") && Time.time >= fireRateCooldown)
                        {
                            if (scoped)
                            {
                                fireRateCooldown = Time.time + 1f / fireRate;
                                Shoot(currentChargeDamage);
                                currentChargeDamage = damage;
                                scopedDamageFillAmount = 0;
                            }
                            else
                            {
                                fireRateCooldown = Time.time + 1f / fireRate;
                                Shoot(damage);
                            }
                        }
                        if (Input.GetButtonUp("Fire2"))
                        {
                            currentChargeDamage = damage;
                            scopedDamageFillAmount = 0;
                            fireRateFill.fillAmount = 1;
                        }
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
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    UIManager.instance.NewNotification("Out of ammo!", 0f, 2f, 2.5f, 6, Color.black);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                if (currentClipAmmo != maxClipAmmo)
                {
                    StartCoroutine(Reload(reloadTime));
                }
                else
                {
                    UIManager.instance.NewNotification("Ammo full!", 0f, 2f, 2.5f, 6, Color.black);
                }
            }

            if (scopeCamera != null)
            {
                if (Input.GetButtonDown("Fire2") || Input.GetButtonUp("Fire2"))
                {
                    Scope();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (reloading)
                {
                    UIManager.instance.NewNotification("Reloading!", 0f, 2f, 2.5f, 6, Color.black);
                }
            }
        }
    }

    private void LateUpdate()
    {
        Recoil();
    }

    private void Shoot(float damage)
    {
        fireRateCooldownFillAmount = 0;

        currentScreenRecoil += verticalScreenRecoilDistance;
        currentGunRecoil += horizontalGunRecoilDistance;
        currentClipAmmo--;

        Vector3 spray = new Vector3();
        if (sprayPerShotIncreaseAmount > 0)
        {
            lastSpray += new Vector3(sprayPerShotIncreaseAmount, sprayPerShotIncreaseAmount);
            spray = transform.right * (Random.Range(-lastSpray.x, lastSpray.x)) + transform.up * (Random.Range(-lastSpray.y, lastSpray.y));
        }
        else
        {
            spray = transform.right * (Random.Range(-horizontalSpray, horizontalSpray)) + transform.up * (Random.Range(-verticalSpray, verticalSpray));
        }

        Transform raycastOrigin;
        if (scoped)
        {
            raycastOrigin = scopeCamera.transform;
        }
        else
        {
            raycastOrigin = mainCam;
        }

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward + spray, out hit, fireDistance)) //, 99, QueryTriggerInteraction.Ignore
        {
            Enemy target = hit.collider.GetComponent<Enemy>();
            if (target != null)
            {
                target.Hit(damage);
            }

            if (hit.collider.gameObject != null)
            {
                if (target != null)
                {
                    Instantiate(bulletHitEffect, hit.point, Quaternion.identity, target.transform);
                }
                else
                {
                    Instantiate(bulletHitEffect, hit.point, Quaternion.identity);
                }
            }

            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null && bulletHitForce > 0)
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(-hit.normal * bulletHitForce);
            }
        }

        if (fovKick > 0)
        {
            mainCamComponent.fieldOfView = defaultFov + fovKick;
        }

        gunMovement.BumpVertical(verticalBumpOnFire, verticalBumpSpeed);
        gunMovement.BumpHorizontal(horizontalGunRecoilDistance, horizontalGunRecoilSpeed);

        UIManager.instance.AmmoTextEffect(ammoTextScaleMultiplier);
    }

    private void Recoil()
    {
        if (screenRecoilMode != ScreenRecoilMode.None)
        {
            if (currentScreenRecoil > 0)
            {
                Quaternion maxRecoil = new Quaternion();
                switch (screenRecoilMode)
                {
                    case ScreenRecoilMode.Up:
                        maxRecoil = Quaternion.Euler(-currentScreenRecoil - verticalScreenRecoilDistance, 0, 0);
                        break;
                    case ScreenRecoilMode.Left:
                        maxRecoil = Quaternion.Euler(0, -currentScreenRecoil - horizontalScreenRecoilDistance, 0);
                        break;
                    case ScreenRecoilMode.Right:
                        maxRecoil = Quaternion.Euler(0, -(-currentScreenRecoil - horizontalScreenRecoilDistance), 0);
                        break;
                    case ScreenRecoilMode.UpLeft:
                        maxRecoil = Quaternion.Euler(-currentScreenRecoil - verticalScreenRecoilDistance, -currentScreenRecoil - horizontalScreenRecoilDistance, 0);
                        break;
                    case ScreenRecoilMode.UpRight:
                        maxRecoil = Quaternion.Euler(-currentScreenRecoil - verticalScreenRecoilDistance, -(-currentScreenRecoil - horizontalScreenRecoilDistance), 0);
                        break;
                    case ScreenRecoilMode.RandomUp:
                        maxRecoil = Quaternion.Euler(-currentScreenRecoil - Random.Range(0, verticalScreenRecoilDistance), 0, 0);
                        break;
                    case ScreenRecoilMode.RandomLeft:
                        maxRecoil = Quaternion.Euler(0, -currentScreenRecoil - Random.Range(0, horizontalScreenRecoilDistance), 0);
                        break;
                    case ScreenRecoilMode.RandomRight:
                        maxRecoil = Quaternion.Euler(0, -(-currentScreenRecoil - Random.Range(0, horizontalScreenRecoilDistance)), 0);
                        break;
                    case ScreenRecoilMode.RandomUpLeftRight:
                        float f = Random.value;
                        if (f < 0.5f)
                        {
                            maxRecoil = Quaternion.Euler(-currentScreenRecoil - Random.Range(0, verticalScreenRecoilDistance), Random.Range(0, horizontalScreenRecoilDistance), 0);
                        }
                        else
                        {
                            maxRecoil = Quaternion.Euler(-currentScreenRecoil - Random.Range(0, verticalScreenRecoilDistance), Random.Range(0, -horizontalScreenRecoilDistance), 0);
                        }
                        break;
                    case ScreenRecoilMode.None:
                        break;
                }

                mainCamHolder.transform.localRotation = Quaternion.Slerp(mainCamHolder.transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);

                if (canRecoilDown)
                {
                    currentScreenRecoil -= Time.deltaTime * currentRecoilFallBackSpeed;
                }
            }
            else
            {
                if (canRecoilDown)
                {
                    currentScreenRecoil = 0;

                    Quaternion minRecoil = Quaternion.Euler(mainCamHolder.transform.localRotation.x, mainCamHolder.transform.localRotation.y, mainCamHolder.transform.localRotation.z);

                    mainCamHolder.transform.localRotation = Quaternion.Slerp(mainCamHolder.transform.localRotation, minRecoil, Time.deltaTime * recoilFallBackSpeed);
                }
            }
        }
    }

    private IEnumerator Reload(float reloadTime)
    {
        reloading = true;
        reloadCooldownFillAmount = 0;
        canRecoilDown = true;


        if (sprayPerShotIncreaseAmount > 0)
        {
            lastSpray = Vector3.zero;
        }

        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = maxClipAmmo - currentClipAmmo;

        currentAmmo -= ammoNeeded;
        currentClipAmmo += ammoNeeded;

        fireRateCooldownFillAmount = 1;
        reloading = false;
    }

    private void Sway()
    {
        if (lookAtGunMovement)
        {
            transform.LookAt(movementLookAtObject);
        }

        // Lissajous curve
        float t = Time.time;
        float x = Mathf.Sin(t) * swayHorizontal;
        float y = Mathf.Cos(t * swayVerticalA) * swayVerticalB;
        transform.localPosition = new Vector3(x, y, 0) + movementLerpOffset;
    }

    private void FireRateToUI()
    {
        if (fireRateFill != null)
        {
            if (!reloading)
            {
                fireRateFill.color = fireRateCrosshairColor;

                if (fireMode == FireMode.Charge && scoped)
                {
                    if (scopedDamageFillAmount < 1)
                    {
                        scopedDamageFillAmount = (currentChargeDamage - damage) / (maxChargedDamage - damage);
                        fireRateFill.fillAmount = scopedDamageFillAmount;
                    }
                    else
                    {
                        scopedDamageFillAmount = 1;
                    }
                }
                else
                {
                    if (fireRateCooldownFillAmount < 1)
                    {
                        fireRateCooldownFillAmount += Time.deltaTime / (fireRateCooldown - Time.time);
                        fireRateFill.fillAmount = fireRateCooldownFillAmount;
                    }
                    else
                    {
                        fireRateCooldownFillAmount = 1;
                    }
                }
            }
            else
            {
                fireRateFill.color = reloadCooldownCrosshairColor;

                reloadCooldownFillAmount += Time.deltaTime / reloadTime;
                fireRateFill.fillAmount = reloadCooldownFillAmount;
            }
        }
    }

    private void Scope()
    {
        if (scopeCamera != null)
        {
            if (Input.GetButton("Fire2"))
            {
                scoped = true;

                gunMovement.enabled = false;
                lookAtGunMovement = false;
                weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, lerpToBeforeScope, Time.deltaTime * scopeSpeed);

                if (Vector3.Distance(lerpToBeforeScope, weaponHolder.localPosition) < 0.1f && !scopeCamera.activeInHierarchy)
                {
                    scopeCamera.SetActive(true);
                }

                playerController.lookSensitivity = defaultMouseSens * scopedMouseSensMultiplier;
            }
            else
            {
                if (Vector3.Distance(weaponHolderDefaultPos, weaponHolder.localPosition) > 0.1f)
                {
                    scoped = false;

                    weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, weaponHolderDefaultPos, Time.deltaTime * scopeSpeed);
                    lookAtGunMovement = true;
                    gunMovement.enabled = true;

                    scopeCamera.SetActive(false);
                }

                playerController.lookSensitivity = defaultMouseSens;
            }
        }
    }

    private void FOV()
    {
        if (mainCamComponent.fieldOfView > defaultFov)
        {
            mainCamComponent.fieldOfView -= Time.deltaTime * fovFallbackSpeed;
        }
        else
        {
            mainCamComponent.fieldOfView = defaultFov;
        }
    }

    private void OnEnable()
    {
        fireRateCooldownFillAmount = 1;
        scopedDamageFillAmount = 1;
        fireRateFill.fillAmount = 1;
        mainCamComponent.fieldOfView = defaultFov;
    }

    private void OnDisable()
    {
        if (reloading)
        {
            reloading = false;
        }
    }
}
