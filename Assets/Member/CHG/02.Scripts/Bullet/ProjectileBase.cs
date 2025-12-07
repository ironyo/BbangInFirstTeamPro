using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected MovementRigidbody2D _movementRigidBody;

        public virtual void SetUp(Transform shooter,Transform target, float damage, int maxCount = 1, int index = 0)
        {
            _movementRigidBody = GetComponent<MovementRigidbody2D>();
        }


        public abstract void Process();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnHit(collision);
            //적 체력 감소
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExit(collision);
        }
        protected abstract void OnHit(Collider2D collision);
        protected abstract void OnExit(Collider2D collision);
    }
}