
[System.Serializable]
public class Item
{

    public string itemName;
    public int quantity;

    // Contructor for creating new items.
    // Sets the name and quantity.
    public Item(string newItemName, int newItemQuantity)
    {
        itemName = newItemName;
        quantity = newItemQuantity;
    }
}
