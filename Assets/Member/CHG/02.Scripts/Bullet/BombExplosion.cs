using System.Collections;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Bullet
{
    public class BombExplosion : MonoBehaviour
    {

        private float _t = 0;
        [SerializeField] private Vector2 MaxScale;
        private float _duraction;
        
        public void SetUp(float duraction)
        {
            _duraction = duraction;
        }
        private void Update()
        {
            _t += Time.deltaTime / _duraction;
            transform.localScale = Utils.Lerp(Vector2.zero, MaxScale, _t);

            if (_t > 1)
            {
                Destroy(gameObject);
            }
        }

    }
}