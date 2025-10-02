using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AlbaSelectMenu : MonoBehaviour
{
    [SerializeField] private AlbaStetSO _stetSO;
    [SerializeField] private TextMeshProUGUI _nameTex;
    [SerializeField] private TextMeshProUGUI _ageTex;
    [SerializeField] private List<TextMeshProUGUI> _powerTex = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _stetTex = new List<TextMeshProUGUI>();

    private void Start()
    {
        AlbaReroll();
    }

    public void AlbaReroll()
    {
        _nameTex.text = _stetSO.name;
        _ageTex.text = _stetSO.age.ToString();
        for (int i = 0; i < _powerTex.Count; i++)
        {
            _powerTex[i].text = _stetSO._power[i];
        }
        for (int i = 0; i < _stetTex.Count; i++)
        {
            
        }
    }
}
