using UnityEngine;

public class MoneyPlusSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    private float _currentTime;

    private void Start()
    {
        MoneyManager.Instance.AddMultiplier(_data.Value);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _data.Duration)
        {
            TimeEnd();
        }
    }
    private void TimeEnd()
    {
        MoneyManager.Instance.RemoveMultiplier(_data.Value);
        Destroy(gameObject);
    }
}
