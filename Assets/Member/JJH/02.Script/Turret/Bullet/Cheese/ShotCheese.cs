using UnityEngine;
using UnityEngine.InputSystem;

public class ShotCheese : FindCloseEnemy, IShotBullet
{
    [SerializeField] private GameObject cheese;
    private float spread = 15f;

    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
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

        SpawnCheese(rotation);
        SpawnCheese(rotation * Quaternion.Euler(0, 0, -spread));
        SpawnCheese(rotation * Quaternion.Euler(0, 0, spread));
    }

    private void SpawnCheese(Quaternion quaternion)
    {
        Instantiate(cheese, transform.position, quaternion);
    }
}
