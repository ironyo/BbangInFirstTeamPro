using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    internal class ProjectileShadow : ProjectileBase
    {
        [SerializeField] private Vector2 MaxScale = Vector2.one;        
        [SerializeField] private Vector2 MinScale = Vector2.one * 0.5f; 
        [SerializeField] private float MaxAlpha = 0.7f;                 
        [SerializeField] private float MinAlpha = 0.3f;
        private SpriteRenderer _spren;
        private Vector2 _start;
        private Vector2 _end;
        private Vector2 _originalScale;
        private Transform _target;
        private float _t;
        private float _duration;
        
        public override void SetUp(Transform target, float damage, int maxCount = 1, int index = 0)
        {
            base.SetUp(target, damage);

            _target = target;
            _start = transform.position;
            _end = _target.position;
            _movementRigidBody.MoveTo((target.position - transform.position).normalized);

            float distance = Vector3.Distance(_start, _end);
            
            _duration = distance / _movementRigidBody.MoveSpeed;

            _spren = GetComponent<SpriteRenderer>();
            
            //float angle = Utils.GetAngleFromPosition(_start, _end);
            //transform.rotation = Quaternion.Euler(0, 0, angle);

            _t = 0;
        }

        private void Update()
        {
            Process();
        }

        protected override void OnHit()
        {
        }

        public override void Process()
        {
            _t += Time.deltaTime / _duration;
            float heightRatio = Utils.GetHeightRatio(_t);

            Vector3 currentScale = Vector3.Lerp(MaxScale, MinScale, heightRatio);
            transform.localScale = currentScale;

            Color color = _spren.color;
            color.a = Mathf.Lerp(MaxAlpha, MinAlpha, heightRatio);
            _spren.color = color;

            if (_t > 1)
                Destroy(gameObject);
        }
    }
}
