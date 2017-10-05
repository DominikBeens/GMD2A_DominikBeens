using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public enum UIState
    {
        CannotInteractWithUI,
        CanInteractWithUI
    }
    public UIState uiState;

    [Header("Ammo Text")]
    public GameObject ammoText;
    public Text currentAmmoText;
    public Text maxAmmoText;
    [Space(10)]
    public float maxHorizontalAmmoTextMovement = 2f;
    public float maxVerticalAmmoTextMovement = 2f;
    private Vector3 ammoTextStartPosition;
    [Space(10)]
    public float ammoTextMovementSpeed;
    public float ammotTextFallbackMovementFallBackSpeed;

    [Header("Notification")]
    public GameObject notification;
    public GameObject notificationSpawn;

    [Header("Shop")]
    public GameObject inventoryPanels;

    [Header("Gun")]
    public Image fireRateFill;

    [Header("Altar")]
    public GameObject altarHealth;
    public Image altarHealthFill;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        #endregion

        ammoTextStartPosition = ammoText.transform.localPosition;
    }

    private void Update()
    {
        SetAmmoText();
        AmmoTextMovement();

        if (inventoryPanels.activeInHierarchy)
        {
            uiState = UIState.CanInteractWithUI;
        }
        else
        {
            uiState = UIState.CannotInteractWithUI;
        }
    }

    public void AmmoTextEffect(float f)
    {
        ammoText.GetComponent<RectTransform>().localScale = new Vector2(f, f);
    }

    private void SetAmmoText()
    {
        if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
        {
            ammoText.SetActive(true);

            if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
            {
                currentAmmoText.text = Inventory.instance.playerWeaponContainers[Inventory.weaponSlotIndex].transform.GetChild(0).gameObject.GetComponent<Gun>().currentClipAmmo.ToString();
                maxAmmoText.text = Inventory.instance.playerWeaponContainers[Inventory.weaponSlotIndex].transform.GetChild(0).gameObject.GetComponent<Gun>().currentAmmo.ToString();
            }
        }
        else
        {
            ammoText.SetActive(false);
        }

        if (ammoText.GetComponent<RectTransform>().localScale.x > 1)
        {
            ammoText.GetComponent<RectTransform>().localScale -= new Vector3(Time.deltaTime, Time.deltaTime);
        }
    }

    private void AmmoTextMovement()
    {
        float y = Mathf.Clamp(Input.GetAxis("Mouse X"), -maxHorizontalAmmoTextMovement, maxHorizontalAmmoTextMovement);
        float x = Mathf.Clamp(Input.GetAxis("Mouse Y"), -maxVerticalAmmoTextMovement, maxVerticalAmmoTextMovement);

        Vector3 movementPosition = new Vector3(ammoText.transform.localPosition.x + y, ammoText.transform.localPosition.y + x, ammoText.transform.localPosition.z);

        ammoText.transform.localPosition = Vector3.Lerp(ammoText.transform.localPosition, movementPosition, Time.deltaTime * ammoTextMovementSpeed);

        if (Vector2.Distance(movementPosition, ammoText.transform.localPosition) < 0.1f)
        {
            ammoText.transform.localPosition = Vector3.Lerp(ammoText.transform.localPosition, ammoTextStartPosition, Time.deltaTime * ammotTextFallbackMovementFallBackSpeed);
        }
    }

    public void NewNotification(string text, float startSize, float desiredSize, float maxSize, float growSpeed, Color color)
    {
        GameObject newNotification = Instantiate(notification, notificationSpawn.transform.position, Quaternion.identity);
        newNotification.transform.SetParent(notificationSpawn.transform);

        Notification notificationComponent = newNotification.GetComponent<Notification>();

        notificationComponent.text = text;
        notificationComponent.startSize = startSize;
        notificationComponent.desiredSize = desiredSize;
        notificationComponent.maxSize = maxSize;
        notificationComponent.growSpeed = growSpeed;
        notificationComponent.GetComponent<Text>().color = color;
    }
}
