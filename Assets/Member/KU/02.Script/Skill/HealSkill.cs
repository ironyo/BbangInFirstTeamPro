using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : SlotSkillBase
{
    private List<TurretBase> turrets = new List<TurretBase>();

    [SerializeField] GameObject _healParticle;

    private int _count = 5;
    private int _currentCount = 0;
    [SerializeField] ItemDataSO _data;

    private int _secHeal;

    private void Start()
    {
        _secHeal = (int)_data.Value / (int)_data.Duration;
        StartCoroutine(HealCount());

        turrets.AddRange(Object.FindObjectsByType<TurretBase>(FindObjectsSortMode.None));
        foreach (var item in turrets)
        {
            Instantiate(_healParticle, item.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator HealCount()
    {
        yield return new WaitForSeconds(1);
        TruckHealthManager.Instance.TruckHealAmount(_secHeal);
        _currentCount++;
        if (_currentCount < _count)
            StartCoroutine(HealCount());
        else
            TimeEnd();

    }

    private void TimeEnd()
    {
        Destroy(gameObject);
    }
}
