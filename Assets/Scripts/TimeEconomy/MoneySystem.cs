using UnityEngine;
public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance;
    [SerializeField] private int _startingMoney = 500;
    [SerializeField] private int _currentMoney;
    public int CurrentMoney => _currentMoney;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _currentMoney = _startingMoney;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        Debug.Log("Money added: " + amount + " | Total: " + _currentMoney);
    }
    public bool SpendMoney(int amount)
    {
        if (_currentMoney < amount)
        {
            return false;
        }
        _currentMoney -= amount;
        Debug.Log("Money spent: " + amount + " | Total: " + _currentMoney);
        return true;
    }
    public void LoadMoney(int loadedMoney)
    {
        _currentMoney = loadedMoney;
        Debug.Log("Money loaded: " + _currentMoney);
    }
}
