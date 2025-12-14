using UnityEngine;
using System;

public class GlobalEnemyModifier : MonoBehaviour
{
    public static GlobalEnemyModifier Instance { get; private set; }

    public event Action OnChanged;

    public float GlobalSpeedMultiplier { get; private set; } = 1f;
    public float GlobalDamageMultiplier { get; private set; } = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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

    public void Clear()
    {
        GlobalSpeedMultiplier = 1f;
        GlobalDamageMultiplier = 1f;
        OnChanged?.Invoke();
    }
}
