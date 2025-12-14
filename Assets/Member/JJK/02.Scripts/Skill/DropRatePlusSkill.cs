using UnityEngine;

public class DropRatePlusSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    private float _currentTime;

    private void Start()
    {
        ItemManager.Instance.AddMultiplier(_data.Value);
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
        ItemManager.Instance.RemoveMultiplier(_data.Value);
        //_slot.Clear();
        Destroy(gameObject);
    }
}
