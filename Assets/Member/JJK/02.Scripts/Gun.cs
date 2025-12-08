    using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : FindCloseEnemy, IShotBullet
{
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private float rotationSpeed = 20f;
    
    [Header("반동")]
    [SerializeField] private float recoilBackAmount = 0.1f;
    [SerializeField] private float recoilBackTime = 0.05f;
    [SerializeField] private float recoilReturnTime = 0.15f;

    private Transform _target;
    private float _lastFireTime;
    private Tween _recoilTween;
    private float _desireAngle;
    private float _offset = 90f;

    private void Start()
    {
        _lastFireTime = -gunData.CoolDown; //처음 발사는 쿨타임X
        
        
    }

    private void Update()
    {
        _target = FindCloseEnemyTrans();

        if (_target != null)
        {
            GunRotation();
            
            if (IsAimTarget())
            {
                if (Time.time - _lastFireTime > gunData.CoolDown)
                {
                    ShotBullet();
                    _lastFireTime = Time.time;
                }
            }
        }
    }

    private bool IsAimTarget()
    {
        Vector2 targetDir = _target.transform.position - firePos.position;
        float diff = Vector2.Angle(transform.up, targetDir);
        
        return diff < 3f;
    }

    private void GunRotation()
    {
        Vector2 dir =  _target.transform.position - firePos.position;
        _desireAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - _offset;
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.Euler(0, 0, _desireAngle), Time.deltaTime * rotationSpeed);
    }

    public void ShotBullet()
    {
        for (int i = 0; i < gunData.GetBullet(); i++)
        {
            var bulletData = gunData.DefaultBullet;
            GameObject bullet = Instantiate(bulletData.BulletPrefab, firePos.position, transform.rotation * CalculateAngle(i));
            bullet.GetComponent<JJK_Bullet>().SetData(bulletData, gunData.ThroughFire);
        }
        
        SpawnMuzzleParticle();
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
    
    private void SpawnMuzzleParticle()
    {
        GameObject muzzleParticle = Instantiate(muzzleFlash, firePos.position, Quaternion.Euler(0, 0, _desireAngle + _offset));
        
        int flip = _desireAngle > 0 ? -1 : 1;
        muzzleParticle.transform.localScale = new Vector3(transform.localScale.x, flip * Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }
    
    private void DoRecoil()
    {
        _recoilTween?.Kill();
        Vector3 originPos = transform.localPosition;
        Vector3 recoilPos = originPos - Vector3.up * recoilBackAmount;

        _recoilTween = DOTween.Sequence()
            .Append(transform.DOLocalMove(recoilPos, recoilBackTime))
            .Append(transform.DOLocalMove(originPos, recoilReturnTime));
    }
}
