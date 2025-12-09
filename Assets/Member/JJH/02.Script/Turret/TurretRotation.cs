using DG.Tweening;
using UnityEngine;

public class TurretRotation : FindCloseEnemy
{
    [SerializeField] private Transform turret;
    [SerializeField] private Transform muzzle;

    private ShotCheese shot;
    private Vector3 startPos;

    private void OnEnable()
    {
        shot = GetComponent<ShotCheese>();
        shot.OnShot += Shot;
        startPos = muzzle.transform.localPosition;
    }

    private void OnDisable()
    {
        shot.OnShot -= Shot;
    }

    private void Shot()
    {
        Vector2 dir = FindCloseEnemyTrans().position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);


        muzzle.transform.DOLocalMove(startPos + new Vector3(0, -0.5f, 0), 0.05f)
            .OnComplete(() =>
                muzzle.transform.DOLocalMove(startPos, 0.1f)
            );
    }
}
