using UnityEngine;
using System.Collections.Generic;
public class ShopSystem : MonoBehaviour
{
    public List<ShopItem> items = new List<ShopItem>();
    public void BuyItem(int index)
    {
        ShopItem item = items[index];
        if (MoneySystem.Instance.SpendMoney(item.price))
        {
            InventorySystem.Instance.AddItem(item.itemName, 1);
            Debug.Log("Bought: " + item.itemName);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}