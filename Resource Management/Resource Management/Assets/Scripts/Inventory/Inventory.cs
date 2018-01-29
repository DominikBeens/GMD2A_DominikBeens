using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{

    // Easy inventory, items are not actually visible in the world (prefabs could be added in the Item class tho) so im using strings as items.
    // This makes it easy to loop through and find a specific item.
    public List<Item> items = new List<Item>();

    // This function loops through the inventory and checks for a certain item, if there is the function returns true otherwise it returns false.
    public bool ContainsSpecificItem(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item)
            {
                if (items[i].quantity > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Overloading:
    // Same function as above but with the 'quantity' parameter added for when you also want to check the quantity of an item in the inventory.
    public bool ContainsSpecificItem(string item, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item && items[i].quantity >= quantity)
            {
                return true;
            }
        }

        return false;
    }

    // This function loops through the inventory and checks if the parameter 'item' is already present in the inventory, if it is then add 'quantity' to it.
    // If it isnt then create a new item with the specified quantity.
    public void AddSpecificItem(string item, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item)
            {
                items[i].quantity += quantity;
                return;
            }
        }

        items.Add(new Item(item, quantity));
    }

    // This function loops through the inventory and checks if the parameter 'item' is present in the inventory, if it is then subtract 'quantity' from it.
    public void RemoveSpecificItem(string item, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item)
            {
                items[i].quantity -= quantity;
                return;
            }
        }
    }

    // This function loops through the inventory to search for a specific item specified with the parameter 'item' and then returns that item.
    public Item GetSpecificItem(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item)
            {
                return items[i];
            }
        }

        return null;
    }
}
