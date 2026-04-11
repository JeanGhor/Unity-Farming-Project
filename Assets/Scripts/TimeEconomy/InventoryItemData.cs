[System.Serializable]
public class InventoryItemData{
    public string ItemName;
    public int Quantity;
    public InventoryItemData(string itemName, int quantity)
    {
        ItemName = itemName;
        Quantity = quantity;
    }
}
