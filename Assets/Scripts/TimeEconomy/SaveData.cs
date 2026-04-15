using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int Money;
    public int Hour;
    public int Minute;
    public int DayNumber;
    public Season Season;
    public List<InventoryItemData> inventory = new List<InventoryItemData>();
    public List<int> ShopStock = new List<int>();
}