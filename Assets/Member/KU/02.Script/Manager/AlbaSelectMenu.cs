using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AlbaSelectMenu : MonoBehaviour
{
    [Header("NowPage")]
    [SerializeField] private int thisPage = 1;

    [Header("SO")]
    [SerializeField] private AlbaStetSO _stetSO;

    [Header("UIs")]
    [SerializeField] private TextMeshProUGUI _nameTex;
    [SerializeField] private TextMeshProUGUI _ageTex;
    [SerializeField] private List<TextMeshProUGUI> _powerTex = new();
    [SerializeField] private List<TextMeshProUGUI> _stetTex = new();

    [Header("Power")]
    [SerializeField] private int _powerCount;
    [SerializeField] private List<string> _power = new(0);
    [SerializeField] private List<string> _powerKindString = new(9);
    private Dictionary<float, string> albaPowerDic = new();

    [Header("Stets")]
    [SerializeField] private List<int> _stet = new(3);
    private Dictionary<string, int> stetDic = new();

    private AlbaSelectManager selectManager;

    private void Awake()
    {
        selectManager = GetComponentInParent<AlbaSelectManager>();
        for (int i = 0; i < _powerKindString.Count; i++)
        {
            albaPowerDic.Add(i, _powerKindString[i]);
            stetDic.Add(_powerKindString[i], i);
        }
    }
    private void Start()
    {
        AlbaReroll();
    }

    public void AlbaReroll()
    {
        _stetSO.age = Random.Range(18, 40);
        _stetSO._name = $"{selectManager.albaSungList[Random.Range(0, selectManager.albaSungList.Length - 1)]}{selectManager.albaNameList[Random.Range(0, selectManager.albaNameList.Length-1)]}";
        _powerCount = Random.Range(1, 4);
        _nameTex.text = _stetSO._name;
        _ageTex.text = "나이 : "+ _stetSO.age.ToString();

        for (int i = 0; i < _power.Count; i++)
        {
            _power[i] = PowerStetReroll();
            _powerTex[i].text = i < _powerCount ?  $"특성 0{i+1} : {_power[i]}" : "";
        }
        for (int i = 0; i < _stetTex.Count; i++)
        {
            string str = i == 0 ? "요리속도" : i == 1 ? "이동속도" : "추가 팁";
            _stet[i] = StetStetReroll();
            stetDic[str] = _stet[i];
            _stetTex[i].text = $"{str} : {stetDic[str]} / 100";
        }
    }
    private string PowerStetReroll()
    {
        int radomNum = Random.Range(1, _powerKindString.Count);
        return albaPowerDic[radomNum];
    }
    private int StetStetReroll()
    {
        int totalWeight = (100 * (100 + 1)) / 2;

        int rand = Random.Range(1, totalWeight + 1);
        int cumulative = 0;

        for (int i = 1; i <= 100; i++)
        {
            cumulative += (101 - i);
            if (rand <= cumulative)
                return i;
        }

        return 100;
    }
}