using UnityEngine;

public class Pepperoni : IncreaseSpeed
{
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
            Destroy(gameObject);
    }
}
