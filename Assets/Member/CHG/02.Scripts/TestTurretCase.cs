using System.Collections;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts
{
    public class TestTurretCase : MonoBehaviour
    {
        [SerializeField] private TurretSO _turretSO;

        [ContextMenu("Start")]
        public void TestStart()
        {
            if (_turretSO == null) return;

            GameObject obj = Instantiate(_turretSO.TurretPrefab, transform.position, Quaternion.identity);
            TurretBase turret = obj.GetComponent<TurretBase>();

            turret.Init(_turretSO, null);


        }
    }
}