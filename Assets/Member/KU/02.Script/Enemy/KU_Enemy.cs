using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class KU_Enemy : MonoBehaviour
{
    private int _currentHp = 5;

    public void MinusHP(int damage)
    {
        _currentHp -= damage;
        if(_currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
