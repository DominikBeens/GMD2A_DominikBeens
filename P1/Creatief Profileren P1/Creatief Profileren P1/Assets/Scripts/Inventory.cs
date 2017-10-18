using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public static InventorySlot selectedSlot;

    public List<InventorySlot> weaponSlots = new List<InventorySlot>();
    public static int weaponSlotIndex;

    public List<GameObject> playerWeaponContainers = new List<GameObject>();
    public List<GameObject> allWeapons = new List<GameObject>();

    public GameObject selectedItemOptionsPanel;
    public GameObject inventoryOptions;
    public GameObject shopOptions;
    public GameObject weaponOptions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetWeapon(int i)
    {
        Item buffer = null;
        if (weaponSlots[i].currentItem != null)
        {
            buffer = weaponSlots[i].currentItem;
            Destroy(playerWeaponContainers[i].transform.GetChild(0).gameObject);
        }

        weaponSlots[i].currentItem = selectedSlot.currentItem;
        selectedSlot.currentItem = null;

        GameObject weapon = Instantiate(weaponSlots[i].currentItem.gameObject,
                                        playerWeaponContainers[i].transform.position + weaponSlots[i].currentItem.gameObject.GetComponent<Gun>().basePositionOffset,
                                        Quaternion.identity, playerWeaponContainers[i].transform);

        if (i == weaponSlotIndex)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }

        if (buffer != null)
        {
            AddToInventory(buffer);
        }

        selectedItemOptionsPanel.SetActive(false);
    }

    public void UnEquipWeapon()
    {
        AddToInventory(selectedSlot.currentItem);

        if (selectedSlot == weaponSlots[0])
        {
            Destroy(playerWeaponContainers[0].transform.GetChild(0).gameObject);
        }
        else if (selectedSlot == weaponSlots[1])
        {
            Destroy(playerWeaponContainers[1].transform.GetChild(0).gameObject);
        }
        else if (selectedSlot == weaponSlots[2])
        {
            Destroy(playerWeaponContainers[2].transform.GetChild(0).gameObject);
        }

        selectedSlot.currentItem = null;
        selectedItemOptionsPanel.SetActive(false);
    }

    public void AddToInventory(Item item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].currentItem == null)
            {
                inventorySlots[i].currentItem = item;
                return;
            }

            if (i == inventorySlots.Count - 1 && inventorySlots[i].currentItem != null)
            {
                UIManager.instance.NewNotification("Inventory Full!", 0f, 2f, 2.5f, 6, Color.black);
            }
        }
    }
}
