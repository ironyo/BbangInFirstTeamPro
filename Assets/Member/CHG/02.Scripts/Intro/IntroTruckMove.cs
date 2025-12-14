using DG.Tweening;
using UnityEngine;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class IntroTruckMove : MonoBehaviour
    {
        private StageData _current;
        private StageData _previous;
        public bool IsRunning { get; private set; } = false;
        [SerializeField] private GameObject _truck;
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
            _onStartSceneReady.RaiseEvent();
            seq.Append(_truck.transform.DOMove(_backPos.position, _backSpeed));
            seq.Append(_truck.transform.DOMove(_endPos.position, _endSpeed));
            seq.InsertCallback(1.4f, () =>
            {
                SceneLoadManager.Instance.SceneMove(1);
            });
        }
    }
}