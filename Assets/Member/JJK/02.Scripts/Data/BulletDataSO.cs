using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletDataSO", menuName = "JJK_SO/BulletDataSO")]
public class BulletDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject BulletPrefab { get; set; }
    [field: SerializeField] public GameObject CollisionParticle { get; set; }
    [field: SerializeField] public GameObject DisableParticle { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float LifeTime { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float CameraShakeForce { get; set; }
    [field: SerializeField] public ThroughShotData ThroughShotData { get; set; }
}

[Serializable]
public class ThroughShotData
{
    [field: SerializeField] public BulletDataSO BulletData { get; set; }
    [field: SerializeField] public int BulletCount { get; set; }
    [field: SerializeField] public float SpreadAngle { get; set; }
}
