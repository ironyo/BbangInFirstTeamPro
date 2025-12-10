using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Turret _currentTurret { get; private set; }  = null;
    public void SpawnTurret(Turret _turret)
    {
        if (_currentTurret != null)
        {
            _currentTurret.DeleteTurret();
        }

        Turret _cloned = Instantiate(_turret.gameObject, gameObject.transform).GetComponent<Turret>();
        _cloned.gameObject.transform.localPosition = Vector3.zero;

        _currentTurret = _cloned;

        Debug.Log("ÅÍ·¿ ¼³Ä¡!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
