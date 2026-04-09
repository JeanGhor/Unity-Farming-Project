using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float realSecondsPerGameMinute = 1f;

    [Header("Start Time")]
    [SerializeField] private int startDay = 1;
    [SerializeField] private int startHour = 8;
    [SerializeField] private int startMinute = 0;

    private float timer;

    private int day;
    private int hour;
    private int minute;

    public int Day => day;
    public int Hour => hour;
    public int Minute => minute;

    private void Start()
    {
        day = startDay;
        hour = startHour;
        minute = startMinute;

        BroadcastTime();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= realSecondsPerGameMinute)
        {
            timer -= realSecondsPerGameMinute;
            AdvanceMinute();
        }
    }

    private void AdvanceMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            day++;
        }

        BroadcastTime();
    }

    private void BroadcastTime()
    {
        GameEventSystem.timeChanged.Invoke(day, hour, minute);
    }
}