using UnityEngine;
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour
{
    public List<ShopItem> items = new List<ShopItem>();
    public int playerMoney = 100;

    public void BuyItem(int index)
    {
        ShopItem item = items[index];

        if (playerMoney >= item.price)
        {
            playerMoney -= item.price;
            Debug.Log("Bought: " + item.itemName);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}