using System;
using System.Collections;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public class HitParticle : MonoBehaviour, IRecycleObject
    {
        public Action<IRecycleObject> Destroyed { get; set; }

        public GameObject GameObject => gameObject;
        [SerializeField] private float LifeTime;
        private float _t = 0;
        private void OnEnable()
        {
            _t = 0;
        }

        private void Update()
        {
            _t += Time.deltaTime;
            if (_t > LifeTime)
            {
                Destroyed?.Invoke(this);
            }
        }
    }
}