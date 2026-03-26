using UnityEngine;
using UnityEngine.Events;

public class ChangeOfTimeHandler : MonoBehaviour
{
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
        if (hour == 6 ) return TimePhase.Morning;
        if (hour == 12) return TimePhase.Afternoon;
        if (hour == 18) return TimePhase.Evening;
        return TimePhase.Night;
    }

    private void ApplyPhaseChange(TimePhase phase)
    {
        Debug.Log(phase.ToString());
    }
}