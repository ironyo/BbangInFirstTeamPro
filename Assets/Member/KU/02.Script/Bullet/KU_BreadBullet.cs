using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class KU_BreadBullet : KU_Bullet
{
    [SerializeField] private GameObject _explosionPref;
    [SerializeField] private Vector3 _explosionSize = new Vector3(3, 3, 3);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (enemy != targetEnemy) return;

            enemy.MinusHP(3);
            GameObject obj = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            obj.transform.localScale = _explosionSize;
            BoomParticle();
            StartCoroutine(DestroyObj(obj));
            Destroy(gameObject);
            return;
        }
    }
    
    private IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}