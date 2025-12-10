using DG.Tweening;
using UnityEngine;

public class Gun : TurretBase
{
    
    [SerializeField] private Transform firePos;
    [SerializeField] private float rotationSpeed = 20f;
    
    [Header("반동")]
    [SerializeField] private float recoilBackAmount = 0.1f;
    [SerializeField] private float recoilBackTime = 0.05f;
    [SerializeField] private float recoilReturnTime = 0.15f;
    
    private Tween _recoilTween;
    private float _offset = 90f;
    
    public override void Shoot()
    {
        for (int i = 0; i < _gunData.GetBullet(); i++)
        {
            var bulletData = _gunData.DefaultBullet;
            var bullet = BulletPoolManager.Instance.Get(bulletData.BulletPrefab).GameObject;

            bullet.transform.position = firePos.position;
            bullet.transform.rotation = transform.rotation * CalculateAngle(i);
            bullet.GetComponent<JJK_Bullet>().SetData(bulletData, _gunData.ThroughFire);
        }
        
        SpawnMuzzleParticle();
        CameraShake.Instance.ImpulseForce(_gunData.CameraShakeForce);
        //DoRecoil();
    }

    private Quaternion CalculateAngle(float num)
    {
        float spreadAngle = 0;

        if (_gunData.MultiFire)
            spreadAngle = Mathf.Lerp(-_gunData.SpreadAngle, _gunData.SpreadAngle, num / (_gunData.GetBullet() - 1));
        
        return Quaternion.Euler(0, 0, spreadAngle - _offset);
    }
    
    private void SpawnMuzzleParticle()
    {
        GameObject muzzleParticle = Instantiate(_gunData.MuzzleFlash, firePos.position, transform.rotation);
        float angle = transform.rotation.z;
        
        int flip = angle > 90 && angle < 180 ? -1 : 1;
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
