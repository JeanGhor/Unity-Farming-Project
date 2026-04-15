using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField] private float transitionDuration = 3f; 

    private TimePhase currentPhase;
    private bool hasInitialized = false;

    private Color morningColor = new Color(0.8f, 0.9f, 1f);
    private Color afternoonColor = new Color(1f, 1f, 0.85f);
    private Color eveningColor = new Color(1f, 0.7f, 0.4f);
    private Color nightColor = new Color(0.05f, 0.05f, 0.2f);

    private void OnEnable()
    {
        GameEventSystem.timeChanged.AddListener(OnTimeChanged);
    }

    private void OnDisable()
    {
        GameEventSystem.timeChanged.RemoveListener(OnTimeChanged);
    }

    private void OnTimeChanged(int hour, int minute,int day)
    {
        TimePhase newPhase = GetPhase(hour, minute);

        if (!hasInitialized || newPhase != currentPhase)
        {
            currentPhase = newPhase;
            hasInitialized = true;
            StopAllCoroutines();
            StartCoroutine(TransitionToColor(GetColorForPhase(newPhase)));
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

    private Color GetColorForPhase(TimePhase phase)
    {
        switch (phase)
        {
            case TimePhase.Morning: return morningColor;
            case TimePhase.Afternoon: return afternoonColor;
            case TimePhase.Evening: return eveningColor;
            case TimePhase.Night: return nightColor;
            default: return morningColor;
        }
    }

    private IEnumerator TransitionToColor(Color targetColor)
    {
        Color startColor = backgroundImage.color;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            backgroundImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        backgroundImage.color = targetColor;
    }
}