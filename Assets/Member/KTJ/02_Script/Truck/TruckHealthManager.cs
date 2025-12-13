using UnityEngine;

public class TruckHealthManager : MonoSingleton<TruckHealthManager>
{
    [Header("Event")]
    [SerializeField] private EventChannelSO _onGameOver;
    [SerializeField] private EventChannelSO_T<int> _onHealthChange;

    private int _currentHealth = 100;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, 100); }
    }

    private void Start()
    {
        TruckHit(50);
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
    }

    public void TruckHeal()
    {
        TruckHit(-100); // 피 만땅으로 채우기
    }
}
