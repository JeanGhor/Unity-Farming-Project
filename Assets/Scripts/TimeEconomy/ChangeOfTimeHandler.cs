using UnityEngine;
using UnityEngine.UI;

public class ChangeOfTimeHandler : MonoBehaviour
{
    public enum TimePhase
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    [SerializeField] private Image backgroundImage;

    private TimePhase currentPhase;
    private bool hasInitialized = false;

    private void OnEnable()
    {
        GameEventSystem.timeChanged.AddListener(OnTimeChanged);
    }

    private void OnDisable()
    {
        GameEventSystem.timeChanged.RemoveListener(OnTimeChanged);
    }

    private void OnTimeChanged(int day, int hour, int minute)
    {
        TimePhase newPhase = GetPhase(hour, minute);

        if (!hasInitialized || newPhase != currentPhase)
        {
            currentPhase = newPhase;
            hasInitialized = true;
            ApplyPhaseChange(newPhase);
        }
    }

    private TimePhase GetPhase(int hour, int minute)
    {
        int totalMinutes = hour * 60 + minute;

        if (totalMinutes >= 360 && totalMinutes < 720)   // 06:00 -> 11:59
            return TimePhase.Morning;

        if (totalMinutes >= 720 && totalMinutes < 1020)  // 12:00 -> 16:59
            return TimePhase.Afternoon;

        if (totalMinutes >= 1020 && totalMinutes < 1260) // 17:00 -> 20:59
            return TimePhase.Evening;

        return TimePhase.Night;
    }

    private void ApplyPhaseChange(TimePhase phase)
    {
        if (backgroundImage == null) return;

        switch (phase)
        {
            case TimePhase.Morning:
                backgroundImage.color = new Color(0.8f, 0.9f, 1f);
                break;

            case TimePhase.Afternoon:
                backgroundImage.color = new Color(1f, 1f, 0.85f);
                break;

            case TimePhase.Evening:
                backgroundImage.color = new Color(1f, 0.7f, 0.4f);
                break;

            case TimePhase.Night:
                backgroundImage.color = new Color(0.05f, 0.05f, 0.2f);
                break;
        }
    }
}