using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected MovementRigidbody2D _movementRigidBody;

        public virtual void SetUp(Transform target, float damage, int maxCount=1, int index = 0)
        {
            _movementRigidBody = GetComponent<MovementRigidbody2D>();
        }

        protected void SetUp()
        {
            Process();
        }

        public abstract void Process();

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Enemy"))
        //    {
        //        OnHit(collision);
                
                
        //        Destroy(gameObject);

        //        //적 체력 감소
        //    }
        //}

        protected abstract void OnHit();
    }
}