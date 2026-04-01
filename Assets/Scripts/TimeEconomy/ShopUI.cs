using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI shopStatusText;
    [SerializeField] private Button closeButton;

    private bool _isShopOpen = false;
    private bool _isShopAvailable = true;

    private void OnEnable()
    {
        GameEventSystem.timeChanged.AddListener(OnTimeChanged);
    }

    private void OnDisable()
    {
        GameEventSystem.timeChanged.RemoveListener(OnTimeChanged);
    }

    private void Start()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseShop);

        UpdateStatusText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ToggleShop();
    }

    public void ToggleShop()
    {
        if (!_isShopAvailable)
        {
            Debug.Log("Shop is closed right now.");
            UpdateStatusText();
            return;
        }

        _isShopOpen = !_isShopOpen;

        if (shopPanel != null)
            shopPanel.SetActive(_isShopOpen);

        UpdateStatusText();
    }

    public void CloseShop()
    {
        _isShopOpen = false;

        if (shopPanel != null)
            shopPanel.SetActive(false);

        UpdateStatusText();
    }

    private void OnTimeChanged(int hour)
    {
        if (hour == 0)
        {
            _isShopAvailable = false;
            _isShopOpen = false;

            if (shopPanel != null)
                shopPanel.SetActive(false);
        }
        else
        {
            _isShopAvailable = true;
        }

        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        if (shopStatusText == null) return;

        if (!_isShopAvailable)
            shopStatusText.text = "Shop is closed (Night)";
        else if (_isShopOpen)
            shopStatusText.text = "Shop is open";
        else
            shopStatusText.text = "Press E to open shop";
    }
}