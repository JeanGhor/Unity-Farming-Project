using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    [Header("Time Speed")]
    [SerializeField] private float secondsPerGameMinute = 1f;

    [Header("Start Time")]
    [SerializeField] private int _startHours = 8;
    [SerializeField] private int _startMinutes = 0;

    [Header("Current Time")]
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;
    [SerializeField] private int _days;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeText;

    private float _timer;

    void Start()
    {
        _hours = _startHours;
        _minutes = _startMinutes;
        _days = 1;

        UpdateTimeUI();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= secondsPerGameMinute)
        {
            _timer -= secondsPerGameMinute;
            AdvanceMinute();
        }
    }

    private void AdvanceMinute()
    {
        _minutes++;

        if (_minutes >= 60)
        {
            _minutes = 0;
            _hours++;

            if (_hours >= 24)
            {
                _hours = 0;
                _days++;
            }
        }

        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        if (timeText != null)
        {
            timeText.text = $"Day {_days} - {_hours:00}:{_minutes:00}";
        }
    }
}