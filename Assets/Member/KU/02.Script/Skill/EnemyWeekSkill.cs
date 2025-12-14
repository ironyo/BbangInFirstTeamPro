using UnityEngine;

public class EnemyWeekSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    private float _currentTime;

    private void Start()
    {
        if (InventoryManager.Instance.IsFull()) return;

        GlobalEnemyModifier.Instance.SetGlobalWeaken(_data.Value);
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
        GlobalEnemyModifier.Instance.ClearWeaken();
        Destroy(gameObject);
    }
}
