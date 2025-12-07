using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CheesePuddle : MonoBehaviour
{
    private float lifeTime = 3f;

    private void OnEnable()
    {
        gameObject.transform.DOScale(1.5f, 0.15f);
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //적 슬로우넣기
        }
    }
}
