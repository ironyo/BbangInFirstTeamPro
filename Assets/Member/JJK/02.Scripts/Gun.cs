using DG.Tweening;
using UnityEngine;

public class Gun : TurretBase
{
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform firePos;
    [SerializeField] private float rotationSpeed = 20f;
    
    [Header("반동")]
    [SerializeField] private float recoilBackAmount = 0.1f;
    [SerializeField] private float recoilBackTime = 0.05f;
    [SerializeField] private float recoilReturnTime = 0.15f;
    
    private Tween _recoilTween;
    private float _desireAngle;
    private float _offset = 90f;
    
    public override void Shoot()
    {
        for (int i = 0; i < gunData.GetBullet(); i++)
        {
            var bulletData = gunData.DefaultBullet;
            var bullet = BulletPoolManager.Instance.Get(bulletData.BulletPrefab).GameObject;

            bullet.transform.position = firePos.position;
            bullet.transform.rotation = transform.rotation * CalculateAngle(i);
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
        GameObject muzzleParticle = Instantiate(gunData.MuzzleFlash, firePos.position, Quaternion.Euler(0, 0, _desireAngle + _offset));
        
        int flip = _desireAngle > 0 && _desireAngle < 180 ? -1 : 1;
        muzzleParticle.transform.localScale = new Vector3( transform.localScale.x, flip * Mathf.Abs(transform.localScale.y), transform.localScale.z);
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
