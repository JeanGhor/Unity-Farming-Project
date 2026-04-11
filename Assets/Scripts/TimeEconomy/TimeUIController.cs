using UnityEngine;
using TMPro;

public class TimeDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        GameEventSystem.timeChanged.AddListener(UpdateTimeDisplay);
    }

    private void OnDisable()
    {
        GameEventSystem.timeChanged.RemoveListener(UpdateTimeDisplay);
    }

    private void Start()
    {
        if (timeText == null)
        {
            Debug.LogWarning("TimeDisplayUI: timeText is not assigned.");
        }
    }

    private void UpdateTimeDisplay(int hour, int minute,int day)
    {
        if (timeText == null) return;

        timeText.text = $"Day {day} - {hour:D2}:{minute:D2}";
    }
}