using UnityEngine;
using System.Collections.Generic;
public class ShopSystem : MonoBehaviour
{

    public static ShopSystem Instance;

    public List<ShopItem> items = new List<ShopItem>();

    private List<int> _originalStock = new List<int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameEventSystem.newDayStarted.AddListener(OnNewDay);
    }

    private void OnDisable()
    {
        GameEventSystem.newDayStarted.RemoveListener(OnNewDay);
    }

    private void Start()
    {
        foreach (ShopItem item in items)
            _originalStock.Add(item.stock);
    }

    private void OnNewDay()
    {
        for (int i = 0; i < items.Count; i++)
            items[i].stock = _originalStock[i];

        Debug.Log("Shop restocked for new day!");
    }

    public void BuyItem(int index)
    {
        ShopItem item = items[index];

        if (item.stock <= 0)
        {
            Debug.Log("Item out of stock: " + item.itemName);
            return;
        }

        if (MoneySystem.Instance.SpendMoney(item.price))
        {
            InventorySystem.Instance.AddItem(item.itemName, 1);
            item.stock--;
            Debug.Log("Bought: " + item.itemName + " | Stock left: " + item.stock);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void SellItem(string itemName)
    {
        ShopItem item = items.Find(i => i.itemName == itemName);

        if (InventorySystem.Instance.RemoveItem(itemName, 1))
        {
            int sellPrice = item != null ? item.sellPrice : 0;
            MoneySystem.Instance.AddMoney(sellPrice);
            Debug.Log("Sold: " + itemName + " for $" + sellPrice);
        }
        else
        {
            Debug.Log("Item not in inventory: " + itemName);
        }
    }

    public List<int> GetStockData()
    {
        List<int> stock = new List<int>();
        foreach (ShopItem item in items)
            stock.Add(item.stock);
        return stock;
    }

    public void LoadStockData(List<int> stockData)
    {
        for (int i = 0; i < items.Count && i < stockData.Count; i++)
            items[i].stock = stockData[i];

        Debug.Log("Shop stock loaded.");
    }
}