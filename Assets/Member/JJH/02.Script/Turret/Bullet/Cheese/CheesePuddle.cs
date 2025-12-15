using Assets.Member.CHG._02.Scripts.Pooling;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CheesePuddle : MonoBehaviour, IRecycleObject
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private float lifeTime = 3f;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        spriteRenderer.DOFade(1f, 0f);
        gameObject.transform.DOScale(1f, 0f);
    }

    private void Update()
    {
        rigid.linearVelocity = new Vector2(-10f, 0);
    }

    private void OnEnable()
    {
        gameObject.transform.DOScale(1.5f, 0.15f);
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        spriteRenderer.DOFade(0f, 0.2f).OnComplete(() => Destroyed?.Invoke(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //적 슬로우넣기
        }
    }
}
