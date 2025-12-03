using UnityEngine;

public class Cheese : MonoBehaviour
{
    [SerializeField] private GameObject cheesePuddle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(cheesePuddle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
