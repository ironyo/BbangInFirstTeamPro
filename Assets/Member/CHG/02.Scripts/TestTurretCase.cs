using Assets.Member.CHG._04.SO.Scripts;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts
{
    public class TestTurretCase : MonoBehaviour
    {
        [SerializeField] private GameObject turret;
        [SerializeField] private AffixSO _affixSO;

        [ContextMenu("Start")]
        public void TestStart()
        {
            Instantiate(turret, transform.position, Quaternion.identity);
        }
    }
}