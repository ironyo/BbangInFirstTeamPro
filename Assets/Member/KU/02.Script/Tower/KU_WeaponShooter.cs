using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class KU_WeaponShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform
    List<KU_Enemy> enemyList = new List<KU_Enemy>();

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryShooting();
        }
    }

    private void TryShooting()
    {
        
        Shooting();
    }
    private void Shooting(Transform target)
    {
        KU_Bullet bullet = Instantiate(bulletPref, firePos.position, Quaternion.identity).GetComponent<KU_Bullet>();
        bullet.GetTarget(target);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemyList.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemyList.Remove(other.GetComponent<Enemy>());
    }
}
