using UnityEngine;
using System;

public class GlobalEnemyModifier : MonoSingleton<GlobalEnemyModifier>
{
    public event Action OnChanged;

    public float GlobalSpeedMultiplier { get; private set; } = 1f;
    public float GlobalDamageMultiplier { get; private set; } = 1f;

    public void SetGlobalSlow(float multiplier)
    {
        GlobalSpeedMultiplier = multiplier;
        OnChanged?.Invoke();
    }

    public void SetGlobalWeaken(float multiplier)
    {
        GlobalDamageMultiplier = multiplier;
        OnChanged?.Invoke();
    }

    public void ClearSlow()
    {
        GlobalSpeedMultiplier = 1f;
        OnChanged?.Invoke();
    }
    public void ClearWeaken()
    {
        GlobalDamageMultiplier = 1f;
        OnChanged?.Invoke();
    }
}
