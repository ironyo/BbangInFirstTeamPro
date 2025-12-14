using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TruckHealthManager : MonoSingleton<TruckHealthManager>
{
    [Header("Event")]
    [SerializeField] private EventChannelSO _onGameOver;
    [SerializeField] private EventChannelSO_T<int> _onHealthChange;
    [SerializeField] private Volume _volume;
    private Vignette _vignette;

    private int _currentHealth = 100;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, 100); }
    }

    private void Start()
    {
        if (_volume.profile.TryGet(out _vignette))
        {
            _vignette.intensity.value = 0f; // 0으로 초기회
        }
        else
        {
            Debug.LogError("Vignette가 Volume Profile에 없습니다.");
        }
    }
    public void TruckHit(int Amount)
    {
        CurrentHealth -= Amount;
        Debug.Log("현재 체력: " + CurrentHealth);
        // UI 반영 이벤트 전송
        _onHealthChange.RaiseEvent(CurrentHealth);

        if (CurrentHealth == 0)
        {
            _onGameOver.RaiseEvent();
            // 게임오버 UI 연출
        }
        if (CurrentHealth <= 20)
        {
            _vignette.intensity.value = 0.3f;
        }
        else
        {
            _vignette.intensity.value = 0;
        }
    }

    public void TruckHealAmount(int amount)
    {
        TruckHit(-amount); // 피 만땅으로 채우기
    }

    public void TruckHealAll()
    {
        TruckHit(-100); // 피 만땅으로 채우기
    }
}
