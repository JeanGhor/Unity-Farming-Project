using UnityEngine;

public class ShopTimeController : MonoBehaviour
{
    [Header("Shop Hours")]
    [SerializeField] private int openHour = 8;
    [SerializeField] private int openMinute = 0;
    [SerializeField] private int closeHour = 18;
    [SerializeField] private int closeMinute = 0;

    public bool IsOpen { get; private set; }

    private void OnEnable()
    {
        GameEventSystem.timeChanged.AddListener(OnTimeChanged);
    }

    private void OnDisable()
    {
        GameEventSystem.timeChanged.RemoveListener(OnTimeChanged);
    }

    private void OnTimeChanged( int hour, int minute,int day)
    {
        bool shouldBeOpen = IsWithinOpenHours(hour, minute);

        if (shouldBeOpen != IsOpen)
        {
            IsOpen = shouldBeOpen;

            if (IsOpen)
                Debug.Log("Shop is now OPEN");
            else
                Debug.Log("Shop is now CLOSED");
        }
    }

    private bool IsWithinOpenHours(int hour, int minute)
    {
        int currentTime = hour * 60 + minute;
        int openTime = openHour * 60 + openMinute;
        int closeTime = closeHour * 60 + closeMinute;

        return currentTime >= openTime && currentTime < closeTime;
    }
}