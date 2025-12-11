using Assets.Member.CHG._04.SO.Scripts;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts
{
    public class TestTurretCase : MonoBehaviour
    {
        [SerializeField] private TurretSO _turretSO;
        [SerializeField] private GunDataSO _gunDataSO;
        [SerializeField] private AffixSO _affixSO;
        private GameObject obj;

        [ContextMenu("Start")]
        public void TestStart()
        {
            if (_turretSO != null)
            {
                obj = Instantiate(_turretSO.TurretPrefab, transform.position, Quaternion.identity);
                TurretBase turret = obj.GetComponent<TurretBase>();
                turret.Init(_turretSO);
            }
            else if (_gunDataSO != null)
            {
                obj = Instantiate(_gunDataSO.TurretPrefab, transform.position, Quaternion.identity);
                TurretBase turret = obj.GetComponentInChildren<TurretBase>();
                turret.Init(_gunDataSO);
            }
        }
    }
}