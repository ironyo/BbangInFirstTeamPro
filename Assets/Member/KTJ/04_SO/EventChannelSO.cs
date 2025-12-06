using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannelSO", menuName = "Scriptable Objects/EventChannelSO")]
public class EventChannelSO : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }
}
