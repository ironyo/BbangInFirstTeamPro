using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;

public class KU_Exprison : KU_Bullet
{
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
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            enemy.MinusHP(3);
        }
    }
}