using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public TurretBase _currentTurret { get; private set; }  = null;
    public void SpawnTurret(TurretBase _turret)
    {
        if (_currentTurret != null)
        {
            _currentTurret.DeleteTurret();
        }

        TurretBase _cloned = Instantiate(_turret.gameObject, gameObject.transform).GetComponent<TurretBase>();
        _cloned.gameObject.transform.localPosition = Vector3.zero;

        _currentTurret = _cloned;

        Debug.Log("ÅÍ·¿ ¼³Ä¡!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
