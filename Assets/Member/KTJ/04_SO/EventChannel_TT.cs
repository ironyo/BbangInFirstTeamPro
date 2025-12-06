using System;
using UnityEngine;

public class EventChannel_TT<T1, T2> : ScriptableObject
{
    public event Action<T1,T2> OnEventRaised;
    public void RaiseEvent(T1 value_1, T2 value_2)
    {
        OnEventRaised?.Invoke(value_1, value_2);
    }
}
