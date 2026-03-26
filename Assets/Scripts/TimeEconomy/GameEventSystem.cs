using UnityEngine;
using UnityEngine.Events;
public static class GameEventSystem
{
    public static UnityEvent<int> timeChanged = new UnityEvent<int>();
}