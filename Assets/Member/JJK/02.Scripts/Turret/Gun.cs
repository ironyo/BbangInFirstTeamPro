using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Gun : TurretBase
{

    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float shootDelay = 0.1f;

    [Header("반동")]
    [SerializeField] private float recoilBackAmount = 0.1f;
    [SerializeField] private float recoilBackTime = 0.05f;
    [SerializeField] private float recoilReturnTime = 0.15f;

    private Tween _recoilTween;
    private float _offset = 90f;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Shoot()
    {
        SpawnMuzzleParticle();
        CameraShake.Instance.ImpulseForce(_gunData.CameraShakeForce);
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(shootDelay);

        for (int i = 0; i < _gunData.GetBullet(); i++)
        {
            var bulletData = _gunData.DefaultBullet;
            var bullet = BulletPoolManager.Instance.Get(bulletData.BulletPrefab).GameObject;

            bullet.transform.position = _firePos.position;
            bullet.transform.rotation = transform.rotation * CalculateAngle(i);
            bullet.GetComponent<JJK_Bullet>().SetData(bulletData, _gunData.ThroughFire);
        }
    }

    private Quaternion CalculateAngle(float num)
    {
        float spreadAngle = 0;

        if (_gunData.MultiFire)
            spreadAngle = Mathf.Lerp(-_gunData.SpreadAngle, _gunData.SpreadAngle, num / (_gunData.GetBullet() - 1));

        return Quaternion.Euler(0, 0, spreadAngle);
    }

    private void SpawnMuzzleParticle()
    {
        GameObject muzzleParticle = Instantiate(_gunData.MuzzleFlash, _firePos.position, transform.rotation * Quaternion.Euler(0, 0, _offset));
        float angle = transform.rotation.z + _offset;

        int flip = angle > 90 && angle < 270 ? -1 : 1;
        muzzleParticle.transform.localScale = new Vector3(transform.localScale.x, flip * Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }
}
