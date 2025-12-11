using System;
using UnityEngine;

public class TurretSO_TJ : ScriptableObject
{
    [field: SerializeField] public string TurretName { get; private set; }
    [field: SerializeField] public Sprite TurretImage { get; private set; }

    [Range(0f, 100f)]
    [field: SerializeField] public int TurretCost { get; private set; }

    [field: SerializeField] public TurretBase Turret; // 실제 터렛 오브젝트 프리팹?

    //public Action<TurretSO_TJ> OnLabelClick;
}
