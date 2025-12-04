using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class KU_WeaponShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject _shootParticlePref;
    [SerializeField] private Transform firePos;

    [SerializeField] private float angle;
    [SerializeField] private LayerMask _enemyLayer;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryShooting();
        }
    }

    private void TryShooting()
    {
        Shooting(GetNearestTarget());
    }

    Transform GetNearestTarget()
    {
        Collider2D[] hitsList = Physics2D.OverlapCircleAll(transform.position, angle, _enemyLayer);

        Transform nearest = null;
        float nearestDist = -Mathf.Infinity;

        if (hitsList == null) return null;

        foreach (var hit in hitsList)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist > nearestDist)
            {
                nearestDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

    private void Shooting(Transform target)
    {
        if(target == null) return;
        Instantiate(_shootParticlePref, firePos.transform.position, Quaternion.identity, transform);
        KU_Bullet bullet = Instantiate(bulletPref, firePos.position, Quaternion.identity).GetComponent<KU_Bullet>();
        if (target.gameObject.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            bullet.GetTarget(enemy);
        }
    }
}
