using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        PopulateShop();
        UpdateMoneyUI();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < shopSystem.items.Count; i++)
        {
            ShopItem item = shopSystem.items[i];
            GameObject obj = Instantiate(itemPrefab, itemContainer);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = item.itemName + " - $" + item.price;

            Button btn = obj.GetComponentInChildren<Button>();
            int index = i;
            btn.onClick.AddListener(() =>
            {
                shopSystem.BuyItem(index);
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