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
    public float ammoTextMovementFallBackSpeed;

    [Header("Notification")]
    public GameObject notification;
    public GameObject notificationSpawn;

    [Header("Shop")]
    public GameObject inventoryPanels;
    [Space(10)]
    public float maxHorizontalInventoryPanelsMovement = 2f;
    public float maxVerticalInventoryPanelsMovement = 2f;
    private Vector3 inventoryPanelsStartPosition;
    [Space(10)]
    public float inventoryPanelsMovementSpeed;
    public float inventoryPanelsMovementFallBackSpeed;

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
        inventoryPanelsStartPosition = inventoryPanels.transform.localPosition;
    }

    private void Update()
    {
        SetAmmoText();
        AmmoTextMovement();

        if (inventoryPanels.activeInHierarchy)
        {
            uiState = UIState.CanInteractWithUI;
            InventoryPanelsMovement();
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
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        Vector2 movementPosition = new Vector2(ammoText.transform.localPosition.x + y, ammoText.transform.localPosition.y + x);

        ammoText.transform.localPosition = Vector2.Lerp(ammoText.transform.localPosition,
                                                        new Vector2(Mathf.Clamp(movementPosition.x, ammoTextStartPosition.x - maxHorizontalAmmoTextMovement, ammoTextStartPosition.x + maxHorizontalAmmoTextMovement),
                                                                    Mathf.Clamp(movementPosition.y, ammoTextStartPosition.y - maxVerticalAmmoTextMovement, ammoTextStartPosition.y + maxVerticalAmmoTextMovement)),
                                                        Time.deltaTime * ammoTextMovementSpeed);

        if (Vector2.Distance(movementPosition, ammoText.transform.localPosition) < 0.1f)
        {
            ammoText.transform.localPosition = Vector2.Lerp(ammoText.transform.localPosition, ammoTextStartPosition, Time.deltaTime * ammoTextMovementFallBackSpeed);
        }
    }

    private void InventoryPanelsMovement()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        Vector2 movementPosition = new Vector2(inventoryPanels.transform.localPosition.x + y, inventoryPanels.transform.localPosition.y + x);

        inventoryPanels.transform.localPosition = Vector2.Lerp(inventoryPanels.transform.localPosition,
                                                               new Vector2(Mathf.Clamp(movementPosition.x, inventoryPanelsStartPosition.x - maxHorizontalInventoryPanelsMovement, inventoryPanelsStartPosition.x + maxHorizontalInventoryPanelsMovement),
                                                                           Mathf.Clamp(movementPosition.y, inventoryPanelsStartPosition.y - maxVerticalInventoryPanelsMovement, inventoryPanelsStartPosition.y + maxVerticalInventoryPanelsMovement)),
                                                               Time.deltaTime * inventoryPanelsMovementSpeed);

        if (Vector2.Distance(movementPosition, inventoryPanels.transform.localPosition) < 0.1f)
        {
            inventoryPanels.transform.localPosition = Vector2.Lerp(inventoryPanels.transform.localPosition, inventoryPanelsStartPosition, Time.deltaTime * inventoryPanelsMovementFallBackSpeed);
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
