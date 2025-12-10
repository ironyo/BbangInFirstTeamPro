using System;
using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO_TJ", menuName = "Scriptable Objects/TurretSO_TJ")]
public class TurretSO_TJ : ScriptableObject
{
    [field:SerializeField] public string TurretName { get; private set; }
    [field: SerializeField] public Sprite TurretImage { get; private set; }

    [Range(0f, 100f)]
    [field: SerializeField] public int TurretCost { get; private set; }

    [field: SerializeField] public Turret Turret; // 실제 터렛 오브젝트 프리팹?

    //public Action<TurretSO_TJ> OnLabelClick;
}
