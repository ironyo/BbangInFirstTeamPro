using System;
using UnityEngine;

public class TruckManager : MonoSingleton<TruckManager>
{
    private int _maxTruckCount = 10;

    private int _curHealth = 100;
    public int CurHealth
    {
        get => _curHealth;
        private set => _curHealth = Mathf.Clamp(value, 0, 100);
    }

    private int _curTruckCount = 1;
    public int CurTruckCount
    {
        get => _curTruckCount;
        private set => _curTruckCount = Mathf.Clamp(value, 1, _maxTruckCount);
    }
}
