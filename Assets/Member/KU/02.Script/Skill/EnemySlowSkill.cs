using System.Threading.Tasks;
using UnityEngine;

public class EnemySlowSkill : SlotSkillBase
{
    [SerializeField] ItemDataSO _data;

    private float _currentTime;

    private void Start()
    {
        GlobalEnemyModifier.Instance.SetGlobalSlow(_data.Value);
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
        GlobalEnemyModifier.Instance.ClearSlow();
        Destroy(gameObject);
    }
}