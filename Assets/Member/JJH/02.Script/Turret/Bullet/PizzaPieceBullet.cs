using UnityEngine;

public class PizzaPieceBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
