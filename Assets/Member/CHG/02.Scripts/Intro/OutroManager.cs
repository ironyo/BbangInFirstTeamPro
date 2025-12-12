using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Member.CHG._02.Scripts.Intro
{
    public class OutroManager : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float extraPadding = 1.0f;
        private float objectWidth;

        private int _count = 0;
        private bool _endRoll = false;
        public List<string> strings = new List<string>();

        private MeshRenderer objRenderer;
        private TextMeshPro _tmp;

        private FadeOut _fadeOut;
        [SerializeField] private GameObject _endRollText;
        [SerializeField] private Transform _endPos;
        [SerializeField] private float _endRollTime;
        void Start()
        {
            objRenderer = GetComponent<MeshRenderer>();
            _tmp = GetComponent<TextMeshPro>();
            _fadeOut = GetComponent<FadeOut>();
        }

        void Update()
        {
            if (_endRoll) return;

            // [수정] Update에서는 이동만 처리하고 너비 계산은 ChangeText 후 바로 처리합니다.

            // 1. 왼쪽으로 이동
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 2. 화면 경계 계산
            float dist = transform.position.z - Camera.main.transform.position.z;
            float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            // 3. 오브젝트 재배치 체크
            float halfWidth = objectWidth / 2f;

            if (transform.position.x < leftBorder - halfWidth - extraPadding)
            {
                Vector3 newPos = transform.position;

                // 4. 오른쪽으로 이동 (오른쪽 경계 + 텍스트 반너비 + 여유공간)
                newPos.x = rightBorder + halfWidth + extraPadding;

                transform.position = newPos;

                ChangeText();
            }
        }

        // 텍스트 너비 계산 로직 분리
        private void CalculateObjectWidth()
        {
            // [핵심 수정] TMP의 레이아웃을 강제로 갱신하여 최신 preferredWidth를 얻습니다.
            // UI 텍스트가 아닌 3D 텍스트이므로 렌더러를 사용한 바운드 계산이 가장 정확합니다.

            _tmp.ForceMeshUpdate(); // <- 텍스트 변경 후 너비를 바로 갱신하는 핵심 로직

            // 렌더러를 통해 월드 공간의 실제 렌더링 너비를 가져옵니다.
            if (objRenderer != null)
            {
                objectWidth = objRenderer.bounds.size.x;
            }
            else
            {
                // MeshRenderer가 없는 경우(예: Canvas UI 텍스트일 경우) 대체 로직
                // preferredWidth * 스케일로 월드 너비를 추정
                objectWidth = _tmp.preferredWidth * transform.lossyScale.x;
            }
        }

        private void ChangeText()
        {
            if (_count >= strings.Count) // strings.Count-1이 아닌 strings.Count로 수정
            {
                _endRoll = true;
                EndRollStart();
                return;
            }

            // 1. 텍스트 내용 변경
            _tmp.text = strings[_count];

            // 2. [핵심 수정] 텍스트가 바뀐 직후에 정확한 새 너비를 계산합니다.
            CalculateObjectWidth();

            _count++;
        }

        private void EndRollStart()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(_fadeOut.FadeSet(0, 0.8f));
            seq.Append(_endRollText.transform.DOMoveY(_endPos.position.y, _endRollTime));
            seq.AppendCallback(() => SceneManager.LoadScene(0));

        }
    }
}