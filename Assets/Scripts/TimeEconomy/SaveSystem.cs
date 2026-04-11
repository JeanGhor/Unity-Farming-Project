using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private string _savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
            Debug.Log("Save Path: " + _savePath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.Money = MoneySystem.Instance.CurrentMoney;

        data.Hour = TimeSystem.Instance.Hour;
        data.Minute = TimeSystem.Instance.Minute;
        data.DayNumber = TimeSystem.Instance.DayNumber;
        data.Season = TimeSystem.Instance.CurrentSeason;

        data.inventory = InventorySystem.Instance.GetInventoryData();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_savePath, json);

        Debug.Log("Game Saved Successfully");
    }

    public void LoadGame()
    {
        if (!File.Exists(_savePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(_savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        MoneySystem.Instance.LoadMoney(data.Money);
        TimeSystem.Instance.LoadTime(data.Hour, data.Minute, data.DayNumber, data.Season);
        InventorySystem.Instance.LoadInventoryData(data.inventory);

        Debug.Log("Game Loaded Successfully");
    }

    public void DeleteSave()
    {
        if (File.Exists(_savePath))
        {
            File.Delete(_savePath);
            Debug.Log("Save file deleted.");
        }
    }
}
