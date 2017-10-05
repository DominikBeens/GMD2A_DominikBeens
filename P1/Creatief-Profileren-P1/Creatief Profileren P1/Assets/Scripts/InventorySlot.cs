using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    private Image borderImage;
    private Image slotImage;

    public enum SlotType
    {
        Inventory,
        Shop,
        Weapon
    }
    public SlotType slotType;

    public Item currentItem;

    private void Awake()
    {
        borderImage = transform.GetChild(0).GetComponent<Image>();
        slotImage = transform.GetChild(2).GetComponent<Image>();
    }

    private void Update()
    {
        if (Inventory.selectedSlot == this || Shop.selectedSlot == this)
        {
            borderImage.enabled = true;
        }
        else
        {
            borderImage.enabled = false;
        }

        if (currentItem != null)
        {
            slotImage.sprite = currentItem.inventorySprite;
            slotImage.enabled = true;
        }
        else
        {
            slotImage.enabled = false;
        }
    }

    public void MouseClick()
    {
        SelectItem();

        if (currentItem != null)
        {
            ShowOptions();
        }

        if (currentItem == null && Inventory.instance.selectedItemOptionsPanel.activeInHierarchy)
        {
            Inventory.instance.selectedItemOptionsPanel.SetActive(false);
        }
    }

    private void SelectItem()
    {
        switch (slotType)
        {
            case SlotType.Inventory:
                Shop.selectedSlot = null;
                Inventory.selectedSlot = this;
                break;
            case SlotType.Shop:
                Inventory.selectedSlot = null;
                Shop.selectedSlot = this;
                break;
            case SlotType.Weapon:
                Shop.selectedSlot = null;
                Inventory.selectedSlot = this;
                break;
        }
    }

    private void ShowOptions()
    {
        Inventory.instance.selectedItemOptionsPanel.transform.SetParent(transform);
        Inventory.instance.selectedItemOptionsPanel.transform.position = new Vector2(transform.position.x, transform.position.y + 100);

        Inventory.instance.inventoryOptions.SetActive(false);
        Inventory.instance.shopOptions.SetActive(false);
        Inventory.instance.weaponOptions.SetActive(false);

        switch (slotType)
        {
            case SlotType.Inventory:
                Inventory.instance.inventoryOptions.SetActive(true);
                break;
            case SlotType.Shop:
                Inventory.instance.shopOptions.SetActive(true);
                break;
            case SlotType.Weapon:
                Inventory.instance.weaponOptions.SetActive(true);
                break;
        }

        Inventory.instance.selectedItemOptionsPanel.SetActive(true);
    }
}
