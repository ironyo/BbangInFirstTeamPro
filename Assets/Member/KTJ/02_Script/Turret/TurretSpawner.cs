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
        if (_cloned == null)
        {
            Debug.LogError("_cloned가 널입니다");
        }
        _cloned.gameObject.transform.localPosition = Vector3.zero;

        _currentTurret = _cloned;

        Debug.Log("터렛 설치!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
