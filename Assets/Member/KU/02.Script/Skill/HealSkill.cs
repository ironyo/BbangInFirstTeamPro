using System.Collections;
using UnityEngine;

public class HealSkill : MonoBehaviour
{
    private int _count = 5;
    private int _currentCount = 0;
    [SerializeField] ItemDataSO _data;

    private void Start()
    {
        StartCoroutine(HealCount());
    }

    private IEnumerator HealCount()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("회복메서드");
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
