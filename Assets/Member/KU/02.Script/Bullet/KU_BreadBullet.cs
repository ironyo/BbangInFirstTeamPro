using System.Collections;
using UnityEngine;

public class KU_BreadBullet : KU_Bullet
{
    [SerializeField] private GameObject _explosionPref;
    [SerializeField] private Vector3 _explosionSize = new Vector3(3, 3, 3);
    private bool isAttack = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Customer>(out Customer customer))
            {
                if (!isAttack)
                {
                    if (customer != targetEnemy) return;
                    SoundManager.Instance.PlaySound(soundData);
                    customer.TakeDamage(damage);
                    GameObject obj = Instantiate(_explosionPref, transform.position, Quaternion.identity);
                    obj.transform.localScale = _explosionSize;
                    isAttack = true;
                    BoomParticle();
                    StartCoroutine(DestroyObj(obj));
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    private IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}