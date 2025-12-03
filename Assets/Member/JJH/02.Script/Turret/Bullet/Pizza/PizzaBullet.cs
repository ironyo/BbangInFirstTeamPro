using UnityEngine;

public class PizzaBullet : IncreaseSpeed
{
    [SerializeField] private GameObject childrenBullet;

    private BulletMove bulletMove;

    private void OnEnable()
    {
        bulletMove = GetComponent<BulletMove>();
        bulletSpeed = bulletMove.Speed;
    }

    private void Update()
    {
        if (bulletMove == null)
            return;

        bulletMove.Speed = IncreaseSpeedInTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            for (int i = 0; i <= 360; i += 45)
            {
                Instantiate(childrenBullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            Destroy(gameObject);
        }
    }
}
