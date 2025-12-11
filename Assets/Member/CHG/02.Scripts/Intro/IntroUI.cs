using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class IntroUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uIGroup;
        [SerializeField] private GameObject _titleText;
        [SerializeField] private GameObject _fadeObj;
        [SerializeField] private Transform _ShowPosition;
        [SerializeField] private Transform _HidePosition;
        [SerializeField] private float _showTime;
        [SerializeField] private float _hideTime;
        [SerializeField] private float _textMoveFadding;
        private List<Button> _buttons = new List<Button>();
        private bool _show = false;
        

        private void Awake()
        {
            _buttons = _uIGroup.GetComponentsInChildren<Button>().ToList();
            foreach (var item in _buttons)
                item.interactable = false;
            
        }

        private void Start()
        {
            _titleText.transform.DOMoveX(_titleText.transform.position.x-_textMoveFadding, _showTime);
            _uIGroup.transform.DOMove(_ShowPosition.position, _showTime).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _show = true;
                foreach (var item in _buttons)
                    item.interactable = true;
            });
        }

        public Tween HideUI()
        {
            _titleText.transform.DOMoveX(_titleText.transform.position.x - _textMoveFadding, _showTime);
            return _uIGroup.transform.DOMove(_HidePosition.position, _hideTime);
        }
        public Tween FadeOut()
        {
            return _fadeObj.transform.DOScale(0, 2);
        }
    }
}