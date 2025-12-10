using System.Collections;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public class MovementRigidbody2D : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        private Rigidbody2D rigid2D;
        private Vector3 dir;
        public float MoveSpeed => moveSpeed;

        private void Awake()
        {
            rigid2D = GetComponent<Rigidbody2D>();
        }

        public void MoveTo(Vector3 direction)
        {
            rigid2D.linearVelocity = direction * moveSpeed;
            dir = direction;        
        }

        public void ChangeSpeed(float speed)
        {
            moveSpeed = speed;
            MoveTo(dir);
        }
    }
}