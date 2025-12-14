using UnityEngine;

public class EnemyWeekSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    private float _currentTime;

    private void Start()
    {
        foreach (var item in Customer.All)
        {
            item.SetSlow();
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
    private void TimeEnd()
    {
        Destroy(gameObject);
    }
}
