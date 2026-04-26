using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeVisualController : MonoBehaviour
{
    public enum TimePhase
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    [Header("References")]
    [SerializeField] private Image backgroundImage;

    [Header("Transition")]
    [SerializeField] private float transitionDuration = 3f;

    [Header("Phase Colors")]
    [SerializeField] private Color morningColor = new Color(0.8f, 0.9f, 1f);
    [SerializeField] private Color afternoonColor = new Color(1f, 1f, 0.85f);
    [SerializeField] private Color eveningColor = new Color(1f, 0.7f, 0.4f);
    [SerializeField] private Color nightColor = new Color(0.05f, 0.05f, 0.2f);

    private TimePhase currentPhase;
    private bool hasInitialized;
    private Coroutine transitionRoutine;

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
        if (TimeSystem.Instance != null)
            OnTimeChanged(TimeSystem.Instance.Hour, TimeSystem.Instance.Minute, TimeSystem.Instance.DayNumber);
    }

    private void OnTimeChanged(int hour, int minute, int day)
    {
        if (backgroundImage == null) return;

        TimePhase newPhase = GetPhase(hour, minute);

        if (!hasInitialized || newPhase != currentPhase)
        {
            currentPhase = newPhase;
            hasInitialized = true;

            if (transitionRoutine != null)
                StopCoroutine(transitionRoutine);

            transitionRoutine = StartCoroutine(TransitionToColor(GetColorForPhase(newPhase)));
        }
    }

    private TimePhase GetPhase(int hour, int minute)
    {
        int totalMinutes = hour * 60 + minute;

        if (totalMinutes >= 360 && totalMinutes < 720)
            return TimePhase.Morning;

        if (totalMinutes >= 720 && totalMinutes < 1020)
            return TimePhase.Afternoon;

        if (totalMinutes >= 1020 && totalMinutes < 1260)
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
        transitionRoutine = null;
    }
}
