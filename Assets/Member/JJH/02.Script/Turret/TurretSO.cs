using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "Scriptable Objects/TurretSO")]
public class TurretSO : ScriptableObject
{
    public int damage;
    public float cooltime;
    public float range;
    public GameObject bullet;
}
