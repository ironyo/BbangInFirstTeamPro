using UnityEngine;

[CreateAssetMenu(fileName = "BulletDataSO", menuName = "JJK_SO/BulletDataSO")]
public class BulletDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject BulletPrefab{ get; set; }
    [field: SerializeField] public GameObject CollisionParticle { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float LifeTime { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float CameraShakeForce { get; set; }
}
