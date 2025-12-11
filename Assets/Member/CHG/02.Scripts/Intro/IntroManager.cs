using DG.Tweening;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class IntroManager : MonoBehaviour
    {
        private StageData _current;
        private StageData _previous;
        public bool IsRunning { get; private set; } = false;
        [SerializeField] private GameObject _truck;
        [SerializeField] private GameObject _fadeObj;
        [SerializeField] private Transform _backPos;
        [SerializeField] private Transform _endPos;
        [SerializeField] private float _backSpeed;
        [SerializeField] private float _endSpeed;

        [Header("Event")]
        [SerializeField] private EventChannelSO _onStartSceneReady;


        private void Start()
        {
            CameraEffectManager.Instance.CameraZoom(7, 1f);
            CameraEffectManager.Instance.CameraMoveTarget(CameraEffectManager.Instance.CameraTarget.gameObject);

        }


        public void StartGame()
        {
            Sequence seq = DOTween.Sequence();

            //seq.Append(_introUI.HideUI());
            seq.Append(_truck.transform.DOMove(_backPos.position, _backSpeed));
            seq.Append(_truck.transform.DOMove(_endPos.position, _endSpeed));
            //seq.Append();
            seq.Insert(1.4f, _fadeObj.transform.DOScale(0, 1.6f));

            _onStartSceneReady.RaiseEvent();
        }
        //public void EndStage()
        //{
        //    if (!IsRunning) return;

        //    IsRunning = false;
        //    _clearStage++;
        //    CameraEffectManager.Instance.CameraZoom(5, 1f);
        //}

        //public StageData GetCurrent() => _current;
        //public StageData GetPrevious() => _previous;


    }
}