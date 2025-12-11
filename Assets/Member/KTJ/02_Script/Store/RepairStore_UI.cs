using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RepairStore_UI : Store_UI
{
    [SerializeField] private GameObject UI;
    [SerializeField] private CanvasGroup _repairMan;

    [Header("TruckSlotUI")]
    [SerializeField] private Transform _truckSlotSpawn;
    [SerializeField] private TruckSlot _truckSlotPref;
    [SerializeField] private Button _truckAddBtn;
    private List<GameObject> _truckSlots = new();

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRepairStoreUIReady;
    [SerializeField] private EventChannelSO _onRepairStoreUIClose;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _setTurretOnTruck;

    private DG.Tweening.Sequence _seq;
    private bool _canClickPerson = false;
    private bool _isActiveUI = false;

    private void Update()
    {
        if (_canClickPerson == false && _isActiveUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _canClickPerson = true;
            }
        }
    }

    public override void CloseUI()
    {
        UI.SetActive(false);

        _canClickPerson = false;
        _isActiveUI = false;

        _onRepairStoreUIClose.RaiseEvent();
    }

    public override void OpenUI()
    {
        UI.SetActive(true);
        StartCoroutine(OpenEffect());
        ResetTrucSlotUI();
        _isActiveUI = true;
    }

    private IEnumerator OpenEffect()
    {
        if (_seq != null)
            _seq.Kill();

        SetUIOrizin();

        // 1. 들어오는 애니메이션
        DG.Tweening.Sequence appearSeq = DOTween.Sequence();
        appearSeq.Append(_repairMan.DOFade(1f, 1f));
        appearSeq.Join(_repairMan.transform
            .DORotate(new Vector3(0, 0, 0), 1f)
            .SetEase(Ease.OutElastic));

        yield return appearSeq.WaitForCompletion();

        // 여기서 클릭 기다림
        yield return new WaitUntil(() => _canClickPerson == true);
        _canClickPerson = false;

        // 2. 사라지는 애니메이션
        DG.Tweening.Sequence exitSeq = DOTween.Sequence();
        exitSeq.Append(_repairMan.transform
            .DORotate(new Vector3(0, 0, -90f), 1f)
            .SetEase(Ease.InOutBack));
        exitSeq.Join(_repairMan.DOFade(0f, 1f));

        yield return exitSeq.WaitForCompletion();

        SetTruckUI();
        _onRepairStoreUIReady.RaiseEvent();
    }

    private void SetUIOrizin()
    {
        _repairMan.alpha = 0f;
        _repairMan.transform.rotation = Quaternion.Euler(0f, 0f, -90);
    }

    private void SetTruckUI() // 여기 열때마다 호출해줌.
    {
        ResetTrucSlotUI();

        int _truckCount = TruckManager.Instance.CurTruckCount;

        for (int i = 1; i <= _truckCount + 1; i++)
        {
            if (i == _truckCount + 1)
            {
                if (TruckManager.Instance.CheckIsTruckFull()) continue; // 이미 트럭개수가 최대라면 +버튼 생성안함

                Button btn = Instantiate(_truckAddBtn.gameObject, _truckSlotSpawn).GetComponent<Button>();
                _truckSlots.Add(btn.gameObject);
                btn.onClick.AddListener(() =>
                {
                    TruckManager.Instance.AddTruck();
                    SetTruckUI();
                });
                continue;
            }

            int slotIndex = i; // 클로저 문제 해결하는 핵심

            TruckSlot slot = Instantiate(_truckSlotPref.gameObject, _truckSlotSpawn).GetComponent<TruckSlot>();
            slot.SetSlotNum(slotIndex);

            _truckSlots.Add(slot.gameObject);

            slot.GetAddBtn().onClick.AddListener(() =>
            {
                Debug.Log(slotIndex + "번 트럭에 터렛이 설치됨");
            });

            StartCoroutine(SendTruckSlotSetEvent(slotIndex));
        }
    }

    private IEnumerator SendTruckSlotSetEvent(int slotIndex)
    {
        yield return new WaitForEndOfFrame();

        var turSO = TruckManager.Instance.CheckIdxTurret(slotIndex - 1);

        if (turSO != null)
        {
            _setTurretOnTruck.RaiseEvent(turSO, slotIndex); // 이제 정확한 슬롯으로 전달됨ㄹ
            Debug.Log("이미지 세팅 이벤트 전송" + turSO.TurretName.ToString() + ", " + slotIndex);
        }
    }

    private void ResetTrucSlotUI()
    {
        if (_truckSlots.Count > 0)
        {
            _truckSlots.ForEach((x) => Destroy(x.gameObject));
            _truckSlots.Clear();
        }
    }
}
