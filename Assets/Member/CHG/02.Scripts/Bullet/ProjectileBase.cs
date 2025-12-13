using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected MovementRigidbody2D _movementRigidBody;
        public int Damage;


        [SerializeField] private SoundDataSO soundData;
        [SerializeField] private float hitCooldown = 0.2f;
        private float lastHitTime = -999f;


        public virtual void SetUp(Transform shooter, Transform target)
        {
            _movementRigidBody = GetComponent<MovementRigidbody2D>();
        }


        public abstract void Process();

        private void OnTriggerEnter2D(Collider2D collision)
        {

            //적 체력 감소
            if (collision.CompareTag("Enemy"))
            {
                if (Time.time - lastHitTime < hitCooldown)
                    return;

                lastHitTime = Time.time;
                SoundManager.Instance.PlaySound(soundData);
                collision.gameObject.GetComponent<Customer>().TakeDamage(Damage);
            }
            OnHit(collision);
        }
        protected abstract void OnHit(Collider2D collision);
    }
}