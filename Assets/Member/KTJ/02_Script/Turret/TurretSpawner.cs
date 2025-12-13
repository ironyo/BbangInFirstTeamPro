using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;
    public TurretBase _currentTurret { get; private set; }  = null;
    public void SpawnTurret(TurretBase _turret)
    {
        if (_currentTurret != null)
        {
            _currentTurret.DeleteTurret();
        }

        TurretBase _cloned = Instantiate(_turret.gameObject, _spawnPos).GetComponent<TurretBase>();
        _cloned.gameObject.transform.localPosition = Vector3.zero;

        _currentTurret = _cloned;

        Debug.Log("ÅÍ·¿ ¼³Ä¡!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
