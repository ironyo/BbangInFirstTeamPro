using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotCheese : FindCloseEnemy, IShotBullet
{
    [SerializeField] private GameObject cheese;
    Factory factory;
    private float spread = 15f;

    public Action OnShot;

    private void Start()
    {
        factory = new Factory(cheese, 15);
    }

    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            OnShot?.Invoke();
            ShotBullet();
        }
    }

    public void ShotBullet()
    {
        Transform enemy = FindCloseEnemyTrans();

        if (enemy == null)
            return;

        Vector3 dir = (enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        CameraShake.Instance.ImpulseForce(0.5f);

        SpawnCheese(rotation);
        SpawnCheese(rotation * Quaternion.Euler(0, 0, -spread));
        SpawnCheese(rotation * Quaternion.Euler(0, 0, spread));
    }

    private void SpawnCheese(Quaternion rotation)
    {
        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = transform.position;
        obj.GameObject.transform.rotation = rotation;
    }
}
