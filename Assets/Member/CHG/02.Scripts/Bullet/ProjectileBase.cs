using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected MovementRigidbody2D _movementRigidBody;

        public virtual void SetUp(Transform shooter,Transform target)
        {
            _movementRigidBody = GetComponent<MovementRigidbody2D>();
        }


        public abstract void Process();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnHit(collision);
            //적 체력 감소
        }
        protected abstract void OnHit(Collider2D collision);
    }
}