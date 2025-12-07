using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager_TJa : MonoBehaviour
{
    
    public StoreSO StoreData;
    
    [SerializeField] private GameObject ProductBtnGroup; //상품 판매 버튼 그룹
    [SerializeField] private GameObject RecipeBtnGroup; //레시피 판매 그룹
    private List<StoreRecipeBtn> RecipeBtns = new List<StoreRecipeBtn>(); //레시피 판매 버튼들
    private List<StoreProductBtn> ProductBtns = new List<StoreProductBtn>(); //상품 판매 버튼들
    private int _reRollCount; //리롤 가능 횟수
    private int _reRollPrice; //리롤 비용
    private Sequence WarningShowSeq; //경고 텍스트 팝업 시퀀스
    [SerializeField] private TextMeshProUGUI WarningText; //경고 텍스트

    public Button ReRollBtn; //리롤버튼

    [ContextMenu("Init")]
    public void Init()
    {
        Debug.Log("Init");
        //SO받아와서 랜덤생성
        
        ProductBtns = ProductBtnGroup.GetComponentsInChildren<StoreProductBtn>().ToList();
        
        
        //상품 버튼들 세팅
        for (int i = 0; i < ProductBtns.Count; i++)
        {
            ProductBtns[i].Init(StoreData.Ingredints[Random.Range(0,StoreData.Ingredints.Count)], this);
        }

        //레시피 버튼들 세팅
        for (int i = 0; i < RecipeBtns.Count; i++)
        {
            RecipeBtns[i].Init(StoreData.Recipes[Random.Range(0, StoreData.Ingredints.Count)], this);
        }

        _reRollCount = StoreData.ReRollCount;
        _reRollPrice = StoreData.ReRollPrice;
        ReRollTextSet();

    }

    #region 리롤
    //리롤버튼 클릭시 조건 확인하고 상품 리롤
    public void ReRollProductBtn()
    {
        if (MoneyManager.Instance.Money < _reRollPrice || _reRollCount <= 0)
        {
            WarningTextShow("새로고침이 불가능합니다.");
            return;
        }

        for (int i = 0; i < ProductBtns.Count; i++)
        {
            ProductBtns[i].Init(StoreData.Ingredints[Random.Range(0, StoreData.Ingredints.Count)], this);
        }

        MoneyManager.Instance.SpendMoney(_reRollPrice);

        _reRollCount--;
        _reRollPrice += 3;

        ReRollTextSet();
    }

    private void ReRollTextSet()
    {
        TextMeshProUGUI[] reRollBtnTexts = ReRollBtn.GetComponentsInChildren<TextMeshProUGUI>();
        reRollBtnTexts[0].text = _reRollPrice.ToString();
        reRollBtnTexts[1].text = _reRollCount.ToString();
    }
    #endregion

    //음식 저장 추가
    public void BuyIngredint()
    {
        
    }

    public void WarningTextShow(string text)
    {
        WarningText.text = text;
        WarningShowSeq?.Kill();

        //WarningShowSeq = DOTween.Sequence();
        WarningShowSeq.Append(WarningText.DOFade(1, 0.4f));
        WarningShowSeq.Append(WarningText.DOFade(0, 0.4f));
    }

}
