using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Tabs")]
    [SerializeField] private GameObject buyTab;
    [SerializeField] private GameObject sellTab;
    [SerializeField] private Button buyTabButton;
    [SerializeField] private Button sellTabButton;

    private void OnEnable()
    {
        GameEventSystem.moneyChanged.AddListener(UpdateMoneyUI);
    }

    private void OnDisable()
    {
        GameEventSystem.moneyChanged.RemoveListener(UpdateMoneyUI);
    }

    private void Start()
    {
        buyTabButton.onClick.AddListener(OpenBuyTab);
        sellTabButton.onClick.AddListener(OpenSellTab);

        PopulateBuyTab();
        OpenBuyTab();
        UpdateMoneyUI();
    }

    private void OpenBuyTab()
    {
        buyTab.SetActive(true);
        sellTab.SetActive(false);
    }

    private void OpenSellTab()
    {
        buyTab.SetActive(false);
        sellTab.SetActive(true);
        PopulateSellTab();
    }

    private void PopulateBuyTab()
    {
        foreach (Transform child in itemContainer)
            Destroy(child.gameObject);
        
        
        for (int i = 0; i < shopSystem.items.Count; i++)
        {
            ShopItem item = shopSystem.items[i];
            GameObject obj = Instantiate(itemPrefab, itemContainer);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = item.itemName + " - $" + item.price + " | Stock: " + item.stock;

            Button btn = obj.GetComponentInChildren<Button>();
            int index = i;
            btn.onClick.AddListener(() =>
            {
                shopSystem.BuyItem(index);
                PopulateBuyTab();
                UpdateMoneyUI();
            });
        }
    }
    private void PopulateSellTab()
    {
        foreach (Transform child in sellTab.transform)
            Destroy(child.gameObject);

        List<InventoryItemData> inventory = InventorySystem.Instance.GetInventoryData();

        if (inventory.Count == 0)
        {
            GameObject obj = Instantiate(itemPrefab, sellTab.transform);
            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "No items to sell";
            obj.GetComponentInChildren<Button>().interactable = false;
            return;
        }

        foreach (InventoryItemData invItem in inventory)
        {
            ShopItem shopItem = shopSystem.items.Find(i => i.itemName == invItem.ItemName);
            int sellPrice = shopItem != null ? shopItem.sellPrice : 0;

            GameObject obj = Instantiate(itemPrefab, sellTab.transform);
            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = invItem.ItemName + " x" + invItem.Quantity + " - $" + sellPrice;

            Button btn = obj.GetComponentInChildren<Button>();
            string nameCapture = invItem.ItemName;
            btn.onClick.AddListener(() =>
            {
                shopSystem.SellItem(nameCapture);
                PopulateSellTab();
                UpdateMoneyUI();
            });
        }
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: $" + MoneySystem.Instance.CurrentMoney;
    }
}