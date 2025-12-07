using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepairStore_UI : Store_UI
{
    [SerializeField] private GameObject UI;
    [SerializeField] private CanvasGroup _repairMan;

    [Header("TruckSlotUI")]
    [SerializeField] private Transform _truckSlotSpawn;
    [SerializeField] private TruckSlot _truckSlotPref;
    private List<TruckSlot> _truckSlots = new();


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
    }

    private void SetUIOrizin()
    {
        _repairMan.alpha = 0f;
        _repairMan.transform.rotation = Quaternion.Euler(0f, 0f, -90);
    }

    private void SetTruckUI()
    {
        ResetTrucSlotUI();

        int _truckCount = TruckManager.Instance.CurTruckCount;

        for (int i = 1; i <= _truckCount; i++)
        {
             TruckSlot slot = Instantiate(_truckSlotPref.gameObject, _truckSlotSpawn).GetComponent<TruckSlot>();
             slot.SetSlotNum(i);

            _truckSlots.Add(slot);
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
