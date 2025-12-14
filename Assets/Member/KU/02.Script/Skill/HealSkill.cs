using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class HealSkill : SlotSkillBase
{
    private List<TurretBase> turrets = new List<TurretBase>();

    [SerializeField] GameObject _healParticle;

    private int _count = 5;
    private int _currentCount = 0;
    [SerializeField] ItemDataSO _data;

    private int _secHeal;

    private float _currentTime;

    private void Start()
    {
        if (InventoryManager.Instance.IsFull()) return;

        _secHeal = (int)_data.Value / (int)_data.Duration;
        StartCoroutine(HealCount());

        turrets.AddRange(Object.FindObjectsByType<TurretBase>(FindObjectsSortMode.None));
        foreach (var item in turrets)
        {
            Instantiate(_healParticle, item.transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _data.Duration)
        {
            TimeEnd();
        }
    }

    private IEnumerator HealCount()
    {
        yield return new WaitForSeconds(1);
        TruckHealthManager.Instance.TruckHealAmount(_secHeal);
        _currentCount++;
        if (_currentCount <= _count)
            StartCoroutine(HealCount());

    }

    private void TimeEnd()
    {
        Destroy(gameObject);
    }
}
