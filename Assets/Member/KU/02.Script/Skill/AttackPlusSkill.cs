using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AttackPlusSkill : SlotSkillBase
{
    private List<TurretBase> turrets = new List<TurretBase>();

    [SerializeField] GameObject _burfParticle;

    [SerializeField] ItemDataSO _data;

    [SerializeField] EventChannelSO_T<int> damageSo;
    [SerializeField] EventChannelSO_T<float> timeSo;

    float _currentTime;

    private void Awake()
    {
        damageSo.RaiseEvent((int)_data.Value);
        timeSo.RaiseEvent(_data.Duration);
    }
    private void Start()
    {
        if (InventoryManager.Instance.IsFull()) return;

        turrets.AddRange(Object.FindObjectsByType<TurretBase>(FindObjectsSortMode.None));
        foreach (var item in turrets)
        {
            Instantiate(_burfParticle, item.transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _data.Duration)
        {
            TimeEnd();
        }
    }

    private void TimeEnd()
    {
        Destroy(gameObject);
    }
}
