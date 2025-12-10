using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;

public class KU_Exprison : KU_Bullet
{
    [SerializeField] private int damage = 1;

    private bool isAttack = false;

    private void Start()
    {
        CameraShake.Instance.ImpulseForce(0.5f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<Customer>(out Customer customer))
            {
                customer.TakeDamage(damage);
            }
        }
    }
}