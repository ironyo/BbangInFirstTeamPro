using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform firePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet(firePos.position);
        }
        
        GunRotation();
    }

    private void GunRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir =  mousePos - firePos.position;
        float desireAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, desireAngle);
    }

    private void ShootBullet(Vector3 position)
    {
        for (int i = 0; i < gunData.GetBullet(); i++)
        {
            var bulletData = gunData.DefaultBullet;
            GameObject bullet = Instantiate(bulletData.BulletPrefab, position, transform.rotation * CalculateAngle());
            bullet.GetComponent<Bullet>().SetData(bulletData, gunData.ThroughFire);
        }
    }

    private Quaternion CalculateAngle()
    {
        float spreadAngle = 0;
        
        if (gunData.MultiFire)
            spreadAngle = Random.Range(-gunData.SpreadAngle, gunData.SpreadAngle);
        
        return Quaternion.Euler(0, 0, spreadAngle);
    }
}
