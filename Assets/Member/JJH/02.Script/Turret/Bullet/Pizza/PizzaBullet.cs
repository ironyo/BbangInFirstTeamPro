using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class PizzaBullet : IncreaseSpeed, IRecycleObject
{
    [SerializeField] private GameObject childrenBullet;
    [SerializeField] private GameObject tomatoSauce;

    Factory pizzaPieceFactory;
    Factory tomatoSauceFactory;

    private BulletMove bulletMove;

    public Action<IRecycleObject> Destroyed { get; set; }

    public GameObject GameObject => gameObject;

    private void Start()
    {
        pizzaPieceFactory = new Factory(childrenBullet, 8);
        tomatoSauceFactory = new Factory(childrenBullet, 8);
    }

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

                IRecycleObject pizzaObj = pizzaPieceFactory.Get();
                pizzaObj.GameObject.transform.position = spawnPos;
                pizzaObj.GameObject.transform.rotation = Quaternion.Euler(0, 0, i - 90);

                IRecycleObject tomatoObj = tomatoSauceFactory.Get();
                pizzaObj.GameObject.transform.position = transform.position;
                pizzaObj.GameObject.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            Destroyed?.Invoke(this);
        }
    }
}
