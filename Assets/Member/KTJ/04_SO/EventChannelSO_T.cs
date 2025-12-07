using System;
using UnityEngine;

public class EventChannelSO_T<T> : ScriptableObject
{
    public event Action<T> OnEventRaised;
    public void RaiseEvent(T value)
    {
        OnEventRaised?.Invoke(value);
    }
}
