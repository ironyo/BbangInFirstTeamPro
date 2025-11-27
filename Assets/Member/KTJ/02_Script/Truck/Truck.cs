using System;
using UnityEngine;

[Serializable]
public class Engine // 엔진 초기 세팅값
{
    [Range(0f, 30)]
    [SerializeField] private int _maxSpeed = 10;
    [Range(100f, 500)]
    [SerializeField] private int _maxHealth = 100;
}

public class Truck : MonoBehaviour
{
    public Engine TruckEngine;
    public int _curSpeed { get; private set; } = 0;
    public int _curHealth { get; private set; } = 0;

    public void StartEngine()
    {
        StageManager.Instance.StartStage();
    }

    public void StopEngine()
    {
        StageManager.Instance.EndStage();
    }
}
