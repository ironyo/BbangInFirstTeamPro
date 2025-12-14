using System.Threading.Tasks;
using UnityEngine;

public class EnemySlowSkill : SlotSkillBase
{
    private float _currentTime;

    [SerializeField] ItemDataSO _data;

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