using UnityEngine;
using UnityEngine.InputSystem;

public class AttackPlusSkill : MonoBehaviour
{
    [SerializeField] EventChannelSO_T<int> damageSo;
    [SerializeField] EventChannelSO_T<float> timeSo;

    int _damage = 2;
    float _time = 50;

    float _currentTime;

    private void Awake()
    {
        damageSo.RaiseEvent(_damage);
        timeSo.RaiseEvent(_time);
    }

    private void Update()
    {
        _currentTime = Time.time;
        if(_currentTime >= _time)
        {
            //Destroy(gameObject);
        }
    }
}
