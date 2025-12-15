using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TruckHealth_UI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private EventChannelSO_T<int> _onHealthChange;

    private Coroutine _cor;

    private void OnEnable()
    {
        _onHealthChange.OnEventRaised += UpdateHealth;
    }

    private void OnDisable()
    {
        _onHealthChange.OnEventRaised -= UpdateHealth;
    }
    public void UpdateHealth(int currentH)
    {
        // 외부에서 이벤트로 부를 메서드 (단일 책임)
        if (_cor != null) StopCoroutine(_cor);
        _cor = StartCoroutine(HealthAnimation(_healthBarSlider.value, currentH, 0.2f));
    }

    private IEnumerator HealthAnimation(float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            _healthBarSlider.value = Mathf.Lerp(start, end, t);

            yield return null;
        }

        _healthBarSlider.value = end; // 마지막 값 보정
    }
}
