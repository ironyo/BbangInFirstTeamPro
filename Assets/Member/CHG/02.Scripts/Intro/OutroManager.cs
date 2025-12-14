using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class OutroManager : MonoBehaviour
    {
        [Header("Scroll")]
        public float speed = 2f;

        [Header("Text List")]
        [SerializeField] private List<string> strings = new List<string>();

        [Header("FadeOut")]
        [SerializeField] private FadeOut _fadeOut;
        [SerializeField] private GameObject _endRollText;
        [SerializeField] private Transform _endPos;
        [SerializeField] private float _endRollTime;


        private TextMeshPro _tmp;
        private Camera _cam;

        private float _textWidth;
        private float _leftLimit;
        private float _rightStartX;

        private int _count = 0;
        private bool _endRoll = false;
        GameObject _manager;

        void Awake()
        {
            _tmp = GetComponent<TextMeshPro>();
            _cam = Camera.main;

            ChangeText(); 
        }
        private void Start()
        {
            _manager = GameObject.Find("SceneLoadManager");
        }
        void Update()
        {
            if (_endRoll) return;

            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x < _leftLimit)
            {
                ChangeText();
            }
        }
        private void ChangeText()
        {
            if (_count >= strings.Count)
            {
                _endRoll = true;
                EndRollStart();
                return;
            }

            _tmp.text = strings[_count];

            CalculateObjectWidth();
            ResetToRight();

            _count++;
        }
        private void CalculateObjectWidth()
        {
            _tmp.ForceMeshUpdate();
            _textWidth = _tmp.textBounds.size.x;

            float camHeight = _cam.orthographicSize * 2f;
            float camWidth = camHeight * _cam.aspect;

            _rightStartX = _cam.transform.position.x + camWidth / 2f + _textWidth / 2f;
            _leftLimit = _cam.transform.position.x - camWidth / 2f - _textWidth / 2f;
        }

        private void ResetToRight()
        {
            Vector3 pos = transform.position;
            pos.x = _rightStartX;
            transform.position = pos;
        }

        private void EndRollStart()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(_fadeOut.FadeSet(0, 0.8f));
            seq.Append(_endRollText.transform.DOMoveY(_endPos.position.y, _endRollTime));
            seq.AppendCallback(() => SceneLoadManager.Instance.SceneMove(0));

        }
    }
}