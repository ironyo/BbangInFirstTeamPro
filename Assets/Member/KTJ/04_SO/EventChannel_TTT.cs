using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannel_TTT", menuName = "Scriptable Objects/EventChannel_TTT")]
public class EventChannel_TTT<T1,T2,T3> : ScriptableObject
{
    public event Action<T1, T2, T3> OnEventRaised;
    public void RaiseEvent(T1 value_1, T2 value_2, T3 value_3)
    {
        OnEventRaised?.Invoke(value_1, value_2, value_3);
    }
}
