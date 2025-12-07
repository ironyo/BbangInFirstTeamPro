using System.Threading;
using UnityEngine;

public class KU_BreadBullet : KU_Bullet
{
    [SerializeField] private GameObject _Explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (enemy != targetEnemy) return;

            enemy.MinusHP(5);
            Destroy(gameObject);
            return;
        }
    }
}
