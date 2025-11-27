using System.Text;
using TMPro;
using UnityEngine;

public abstract class MoreInfoUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI MoreInfoUIText; //추가정보 Text
    [SerializeField] protected CanvasGroup MoreUICanvasGroup; //추가정보창 CanvasGroup
    protected StringBuilder _sb; //내용 저장
    private RectTransform _rectTransform;
    

    protected void Awake()
    {
        MoreUICanvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }

    protected void MoreInfoBtnEnter(RectTransform pos)
    {
        MoreUICanvasGroup.alpha = 1;
        _sb.Clear();
        _rectTransform.position = pos.position;


    }
    protected void MoreInfoBtnExit()
    {
        MoreUICanvasGroup.alpha = 0;
    }

}

