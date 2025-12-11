using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class IntroSignMover : MonoBehaviour
    {
        [SerializeField] private GameObject _signObj;

        [SerializeField] private RoadManager _manager;
        private Vector3 _startPos;
        private float _objLength;
        Vector3 leftPos;

        private void Start()
        {
            _objLength = _signObj.GetComponent<SpriteRenderer>().bounds.size.x;
            _startPos = _signObj.transform.position;

            leftPos = Camera.main.ScreenToWorldPoint(
            new Vector3(0f, Screen.height * 0.5f, Camera.main.nearClipPlane)
            );
        }

        private void Update()
        {
            if (_manager.CurrentSpeed <= 0f) return;

            float moveValue = _manager.CurrentSpeed * Time.deltaTime;


            _signObj.transform.position += Vector3.left * moveValue;

            if (_signObj.transform.position.x <= leftPos.x - _objLength / 2)
            {
                _signObj.transform.position = _startPos;
                //_titleObj.transform.position += Vector3.right * _objLength + leftPos;
            }
        }
    }
}