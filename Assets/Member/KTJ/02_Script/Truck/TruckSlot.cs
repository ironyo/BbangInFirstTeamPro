using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TruckSlot : MonoBehaviour
{
    [Header("UI Setting")]
    [SerializeField] private TextMeshProUGUI _truckSlotNumTxt;
    [SerializeField] private Button _addBtn;
    [SerializeField] Image TurretImage;
    [SerializeField] private GameObject _stripEffect;

    [Header("Events")]
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _onTryPurchase;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _setTurretOnTruck;
    [SerializeField] private EventChannelSO _turretDragStart;
    [SerializeField] private EventChannelSO _turretDragEnd;

    private int _slotNum;

    private void OnEnable()
    {
        _setTurretOnTruck.OnEventRaised += TruckUISet;
        _turretDragStart.OnEventRaised += OnDragStart;
        _turretDragEnd.OnEventRaised += OnDragEnd;
    }
    private void OnDisable()
    {
        _setTurretOnTruck.OnEventRaised -= TruckUISet;
        _turretDragStart.OnEventRaised -= OnDragStart;
        _turretDragEnd.OnEventRaised -= OnDragEnd;
    }

    private void OnDestroy()
    {
        _setTurretOnTruck.OnEventRaised -= TruckUISet;
    }
    private void OnDragStart()
    {
        _stripEffect.SetActive(true);
    }

    private void OnDragEnd()
    {
        _stripEffect.SetActive(false);
    }


    public void SetSlotNum(int val)
    {
        _truckSlotNumTxt.text = val + "번";
        _slotNum = val;
    }

    public Button GetAddBtn() => _addBtn;

    private void TruckUISet(TurretSO_TJ turSO, int Idx)
    {
        Debug.Log("일단 슬롯에서 신호는 받았음. MyIdx: " + _slotNum);
        if (turSO == null)
        {
            Debug.Log(Idx + " 아이 씨발 널이잖아 개년아");
        }
        if (Idx == _slotNum)
        {
            TurretImage.sprite = turSO.TurretImage;
            TurretImage.gameObject.SetActive(true);

            Debug.Log("터렛 슬롯에 이미지 세팅을 완료했습니다");
        };
    }

    public void DropTurret(TurretSO_TJ turSO)
    {
        Debug.Log(_slotNum + "번째 트럭에 터렛부착을 시도함");
        //TurretImage.gameObject.SetActive(true);
        //TurretImage.sprite = turSO.TurretImage;

        // TryPurchase 이벤트를 전송함.
        _onTryPurchase.RaiseEvent(turSO, _slotNum);
    }
}
