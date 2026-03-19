using UnityEngine; 

public class TimeSystem : MonoBehaviour
{
    float SECONDS_IN_MINUTE = 1f;

    [Header("Start Time")]
    [SerializeField] private int _startHours = 8;
    [SerializeField] private int _startMinutes = 0;
    [Header("Current Time")]
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;
    [SerializeField] private int _days;
    private float _timer=0f;
    void Start()
    {
        _hours = _startHours;
        _minutes = _startMinutes;
        _days = 0;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        Debug.Log($"Time: {_hours:00}:{_minutes:00} Day: {_days}");
        if (_timer >= SECONDS_IN_MINUTE)
        {
            _timer = 0f;
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
        }
    }
}
