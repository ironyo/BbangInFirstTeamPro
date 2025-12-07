using UnityEngine;

public class PizzaBullet : IncreaseSpeed
{
    [SerializeField] private GameObject childrenBullet;
    [SerializeField] private GameObject tomatoSauce;

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
            CameraShake.Instance.ImpulseForce(3f);

            float offset = 1.5f;
            for (int i = 0; i <= 360; i += 45)
            {
                //각도를 라디안으로 변환
                float radian = i * Mathf.Deg2Rad;
                //벡터로 바꾸기
                Vector3 dir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
                // 현재 위치에 위치 +해주기
                Vector3 spawnPos = transform.position + dir * offset;

                Instantiate(childrenBullet, spawnPos, Quaternion.Euler(0, 0, i - 90));
                Instantiate(tomatoSauce, transform.position, Quaternion.Euler(0, 0, i));
            }

            Destroy(gameObject);
        }
    }
}
