using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Turret : MonoBehaviour
{
    private int _turretLevel;
    public int TurretLevel
    {
        get
        {
            return _turretLevel;
        }
        set
        {
            _turretLevel = Mathf.Clamp(value, 0, _maxTurretLevel);
        }
    }

    private int _maxTurretLevel;

    public void SpawnTurret(Transform _spawnParent)
    {
        OnSpawn();
        GameObject _spawned_T = Instantiate(gameObject, _spawnParent);
        _spawned_T.transform.localPosition = Vector3.zero;
    }

    public void DeleteTurret()
    {
        OnDelete();
        Destroy(gameObject);
    }

    protected abstract void OnSpawn();
    protected abstract void OnDelete();
}