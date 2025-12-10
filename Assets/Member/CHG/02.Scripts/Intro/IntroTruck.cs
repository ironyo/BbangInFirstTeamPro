using System.Collections.Generic;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class IntroTruck : MonoBehaviour
    {

        [SerializeField] private GameObject truckBody;
        [SerializeField] private Transform truckBodySpawnTran;

        private List<(TurretSpawner, TurretSO_TJ)> _truckBodyList = new List<(TurretSpawner, TurretSO_TJ)>();



        private int _curTruckCount = 0;
        public int CurTruckCount
        {
            get => _curTruckCount;
            private set
            {
                AddTruckBody();
            }
        }

        private void Awake()
        {
            CurTruckCount++;
        }

        private void AddTruckBody()
        {
            GameObject _clonedBody = Instantiate(truckBody, truckBodySpawnTran);
            _truckBodyList.Add((_clonedBody.GetComponent<TurretSpawner>(), null));

            _clonedBody.transform.localPosition = new Vector3(((_truckBodyList.Count - 1) * -2.78f), 0, 0);
        }

        public void SetTurret(int SpawnTruckIdx, TurretSO_TJ turSO)
        {
            var old = _truckBodyList[SpawnTruckIdx - 1];

            // 1. 터렛 스폰
            old.Item1.SpawnTurret(turSO.Turret);

            // 2. 터렛 SO 저장 ★★ 필수
            _truckBodyList[SpawnTruckIdx - 1] = (old.Item1, turSO);
        }
    }
}