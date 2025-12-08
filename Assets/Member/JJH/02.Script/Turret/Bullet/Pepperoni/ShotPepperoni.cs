using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotPepperoni : FindCloseEnemy, IShotBullet
{
    [SerializeField] private GameObject pepperoni;
    Factory factory;

    private void Start()
    {
        factory = new Factory(pepperoni, 10);
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
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

        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = transform.position;
        obj.GameObject.transform.rotation = rotation;
    }
}
