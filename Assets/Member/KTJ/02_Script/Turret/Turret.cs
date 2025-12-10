using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Turret : MonoBehaviour
{
    public void SpawnTurret(Transform _spawnParent)
    {
        OnSpawn();
    }

    public void DeleteTurret()
    {
        OnDelete();
        Destroy(gameObject);
    }

    protected abstract void OnSpawn();
    protected abstract void OnDelete();
}