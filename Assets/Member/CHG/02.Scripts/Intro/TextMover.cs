using TMPro;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class TextMover : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private float _padding;
        [SerializeField] private float _speed;
        private void Update()
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

            float dist = transform.position.z - Camera.main.transform.position.z;

            float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            if (transform.position.x < leftBorder - _padding)
            {
                Vector3 newPos = transform.position;
                newPos.x = rightBorder + _padding;
                transform.position = newPos;
            }
        }
    }
}