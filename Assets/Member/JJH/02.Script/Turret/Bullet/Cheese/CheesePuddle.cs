using System.Collections;
using UnityEngine;

public class CheesePuddle : MonoBehaviour
{
    private float lifeTime = 3f;

    private void OnEnable()
    {
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
