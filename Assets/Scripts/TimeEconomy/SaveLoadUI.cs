using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Instance.SaveGame();
    }

    public void LoadGame()
    {
        SaveSystem.Instance.LoadGame();
    }

    public void DeleteSave()
    {
        SaveSystem.Instance.DeleteSave();
    }
}