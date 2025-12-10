using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "C_SO/TurretSO")]
public class TurretSO : ScriptableObject
{
    public Sprite TurretSprite;
    public int TurretPrice;

    public GameObject TurretPrefab;
    public float AttackCoolTime;
    public float AttackRange;
    public bool TargetingClosedEnemy;
}
