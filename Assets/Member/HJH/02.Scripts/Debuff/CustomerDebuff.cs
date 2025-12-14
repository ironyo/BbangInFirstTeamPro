using System;
using UnityEngine;

public class CustomerDebuff : MonoBehaviour
{
    public event Action OnChanged;

    public float SpeedMultiplier { get; private set; } = 1f;
    public float DamageMultiplier { get; private set; } = 1f;

    public void ApplySlow(float multiplier)
    {
        SpeedMultiplier = multiplier;
        OnChanged?.Invoke();
    }

    public void ApplyWeaken(float multiplier)
    {
        DamageMultiplier = multiplier;
        OnChanged?.Invoke();
    }

    public void Clear()
    {
        SpeedMultiplier = 1f;
        DamageMultiplier = 1f;
        OnChanged?.Invoke();
    }
}
