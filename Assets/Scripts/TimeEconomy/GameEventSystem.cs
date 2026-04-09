using UnityEngine.Events;

[System.Serializable]
public class TimeChangedEvent : UnityEvent<int, int, int> { }
public static class GameEventSystem
{
    public static TimeChangedEvent timeChanged = new TimeChangedEvent();
}