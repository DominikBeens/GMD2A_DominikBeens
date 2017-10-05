using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public List<InventorySlot> shopSlots = new List<InventorySlot>();

    public static InventorySlot selectedSlot;

    private void Update()
    {
        if (UIManager.instance.inventoryPanels.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.inventoryPanels.SetActive(false);
            }
        }
    }

    public void BuyItem()
    {
        Inventory.instance.AddToInventory(selectedSlot.currentItem);
        selectedSlot.currentItem = null;
        Inventory.instance.selectedItemOptionsPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.instance.NewNotification("Press E to open up the shop", 0f, 2f, 2.5f, 4, Color.black);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UIManager.instance.inventoryPanels.SetActive(true);
            }
        }
    }

    public void CloseShopButton()
    {
        UIManager.instance.inventoryPanels.SetActive(false);
    }
}
