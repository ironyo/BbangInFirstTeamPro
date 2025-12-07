using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("반동")]
    [SerializeField] private float recoilBackAmount = 0.1f;
    [SerializeField] private float recoilBackTime = 0.05f;
    [SerializeField] private float recoilReturnTime = 0.15f;

    private GameObject _target;
    private float _lastFireTime;
    private Tween recoilTween;


    private void Start()
    {
        _lastFireTime = -gunData.CoolDown; //처음 발사는 쿨타임X
    }

    private void Update()
    {
        _target = FindNearestEnemy();

        if (_target != null)
        {
            GunRotation();
            
            if (IsAimTarget())
            {
                if (Time.time - _lastFireTime > gunData.CoolDown)
                {
                    ShootBullet(firePos.position);
                    _lastFireTime = Time.time;
                }
            }
        }
    }
    
    private GameObject FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, gunData.DetectRange, enemyLayer);

        if (hits.Length == 0)
            return null;

        float minDist = float.MaxValue;
        GameObject nearest = null;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.gameObject;
            }
        }

        return nearest;
    }

    private bool IsAimTarget()
    {
        Vector2 targetDir = _target.transform.position - transform.position;
        float diff = Vector2.Angle(transform.right, targetDir);
        
        return diff < 3f;
    }

    private void GunRotation()
    {
        Vector2 dir =  _target.transform.position - firePos.position;
        float desireAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, desireAngle), Time.deltaTime * rotationSpeed);
    }

    private void ShootBullet(Vector3 position)
    {
        for (int i = 0; i < gunData.GetBullet(); i++)
        {
            var bulletData = gunData.DefaultBullet;
            GameObject bullet = Instantiate(bulletData.BulletPrefab, position, transform.rotation * CalculateAngle(i));
            bullet.GetComponent<JJK_Bullet>().SetData(bulletData, gunData.ThroughFire);
        }
        
        Instantiate(muzzleFlash, firePos.position, transform.rotation);
        CameraShake.Instance.ImpulseForce(gunData.CameraShakeForce);
        DoRecoil();
    }

    private Quaternion CalculateAngle(float num)
    {
        float spreadAngle = 0;

        if (gunData.MultiFire)
            spreadAngle = Mathf.Lerp(-gunData.SpreadAngle, gunData.SpreadAngle, num / (gunData.GetBullet() - 1));
        
        return Quaternion.Euler(0, 0, spreadAngle);
    }
    
    private void DoRecoil()
    {
        recoilTween?.Kill();

        Vector3 originalPos = transform.localPosition;
        Vector3 recoilPos = originalPos - transform.right * recoilBackAmount;
        
        recoilTween = DOTween.Sequence()
            .Append(transform.DOLocalMove(recoilPos, recoilBackTime).SetEase(Ease.OutCubic))
            .Append(transform.DOLocalMove(originalPos, recoilReturnTime).SetEase(Ease.InOutSine));
    }
}
