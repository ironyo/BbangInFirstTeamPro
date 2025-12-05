using UnityEngine;

[CreateAssetMenu(fileName = "GunDataSO", menuName = "JJK_SO/GunDataSO")]
public class GunDataSO : ScriptableObject
{
    [field: SerializeField] public BulletDataSO DefaultBullet { get; set; }
    [field: SerializeField] public GameObject MuzzleFlash { get; set; }
    [field: SerializeField] public float CoolDown { get; set; }
    [field: SerializeField] public float DetectRange { get; set; }
    [field: SerializeField] public float CameraShakeForce { get; set; }
    [SerializeField] private int bulletCount;
    [field: SerializeField] public float SpreadAngle { get; set; }
    [field: SerializeField] public bool MultiFire { get; set; }
    [field: SerializeField] public bool ThroughFire { get; set; }
    
    public int GetBullet()
    {
        if (MultiFire)
            return bulletCount;
        
        return 1;
    }
}
