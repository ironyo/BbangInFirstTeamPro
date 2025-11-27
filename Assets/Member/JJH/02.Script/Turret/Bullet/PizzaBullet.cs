using UnityEngine;

public class PizzaBullet : MonoBehaviour
{
    [SerializeField] private GameObject childrenBullet;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < 8; i += 45)
        {
            Instantiate(childrenBullet, transform.position + new Vector3(10, 0, 0), Quaternion.Euler(0, 0, i));
        }
        Destroy(gameObject);
    }
}
