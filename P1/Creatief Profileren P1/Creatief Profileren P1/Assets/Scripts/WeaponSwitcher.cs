using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public static WeaponSwitcher instance;

    private Transform weaponHolder;

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

        weaponHolder = GameObject.FindWithTag("WeaponHolder").transform;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
            {
                UnequipWeapon();
            }

            if (Inventory.weaponSlotIndex == 0)
            {
                Inventory.weaponSlotIndex = Inventory.instance.weaponSlots.Count - 1;
            }
            else
            {
                Inventory.weaponSlotIndex--;
            }

            UIManager.instance.AmmoTextEffect(1.1f);

            if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
            {
                EquipWeapon();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
            {
                UnequipWeapon();
            }

            if (Inventory.weaponSlotIndex == Inventory.instance.weaponSlots.Count - 1)
            {
                Inventory.weaponSlotIndex = 0;
            }
            else
            {
                Inventory.weaponSlotIndex++;
            }

            UIManager.instance.AmmoTextEffect(1.1f);

            if (Inventory.instance.weaponSlots[Inventory.weaponSlotIndex].currentItem != null)
            {
                EquipWeapon();
            }
        }
    }

    private void EquipWeapon()
    {
        Inventory.instance.playerWeaponContainers[Inventory.weaponSlotIndex].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void UnequipWeapon()
    {
        Inventory.instance.playerWeaponContainers[Inventory.weaponSlotIndex].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void AddToInventory(Weapon weapon)
    {
        foreach (InventorySlot weaponSlot in Inventory.instance.weaponSlots)
        {
            if (weaponSlot.currentItem == null)
            {
                weaponSlot.currentItem = weapon;

                weapon.prefab.SetActive(false);
                weapon.transform.SetParent(weaponHolder);
                weapon.transform.position = Vector3.zero;
                return;
            }
        }
    }
}
