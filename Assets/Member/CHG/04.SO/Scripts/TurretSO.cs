using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "C_SO/TurretSO")]
public class TurretSO : TurretSO_TJ
{
    public GameObject TurretPrefab;
    public float AttackCoolTime;
    public float AttackRange;
    public int AttackPower;
    public bool TargetingClosedEnemy;
}
