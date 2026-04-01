using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeOfTimeHandler : MonoBehaviour
{
    [SerializeField] private Image background;

    private enum TimePhase
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    private TimePhase _currentPhase;

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
        if (background != null)
        {
            background.color = new Color(0.05f, 0.05f, 0.2f);
        }
    }

    private void OnTimeChanged(int hour)
    {
        TimePhase newPhase = GetPhase(hour);

        if (newPhase != _currentPhase)
        {
            _currentPhase = newPhase;
            ApplyPhaseChange(newPhase);
        }
    }

    private TimePhase GetPhase(int hour)
    {
        Debug.Log("Hour: " + hour);

        if (hour >= 6 && hour < 12) return TimePhase.Morning;
        if (hour >= 12 && hour < 18) return TimePhase.Afternoon;
        if (hour >= 18 && hour < 21) return TimePhase.Evening;
        return TimePhase.Night;
    }

    private void ApplyPhaseChange(TimePhase phase)
    {
        Debug.Log(phase.ToString());

        if (background == null) return;

        switch (phase)
        {
            case TimePhase.Morning:
                background.color = new Color(0.6f, 0.8f, 1f);
                break;

            case TimePhase.Afternoon:
                background.color = new Color(0.4f, 0.7f, 1f);
                break;

            case TimePhase.Evening:
                background.color = new Color(1f, 0.5f, 0.3f);
                break;

            case TimePhase.Night:
                background.color = new Color(0.05f, 0.05f, 0.2f);
                break;
        }
    }
}