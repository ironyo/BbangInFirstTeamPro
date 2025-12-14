using UnityEngine;
using UnityEngine.InputSystem;

public class AttackPlusSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    [SerializeField] EventChannelSO_T<int> damageSo;
    [SerializeField] EventChannelSO_T<float> timeSo;

    float _currentTime;

    private void Awake()
    {
        damageSo.RaiseEvent((int)_data.Value);
        timeSo.RaiseEvent(_data.Duration);
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
