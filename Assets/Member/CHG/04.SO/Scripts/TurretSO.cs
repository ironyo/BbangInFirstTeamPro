using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "C_SO/TurretSO")]
public class TurretSO : TurretSO_TJ
{
    public Sprite TurretSprite;
    public int TurretPrice;

    public GameObject TurretPrefab;
    public float AttackCoolTime;
    public float AttackRange;
    public int AttackPower;
    public bool TargetingClosedEnemy;
}
