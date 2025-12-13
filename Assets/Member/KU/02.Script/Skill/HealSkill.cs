using System.Collections;
using UnityEngine;

public class HealSkill : MonoBehaviour
{
    private int _count = 5;
    private int _currentCount = 0;
    [SerializeField] ItemDataSO _data;

    private int _secHeal;

    private void Start()
    {
        _secHeal = (int)_data.Value / (int)_data.Duration;
        StartCoroutine(HealCount());
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
