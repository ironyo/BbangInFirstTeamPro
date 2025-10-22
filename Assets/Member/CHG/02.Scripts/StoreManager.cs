using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Image MoreInfoUI;
    public StoreSO StoreData;
    
    private GameObject ProductBtnGroup;
    private List<StoreProduct> ProductBtns = new List<StoreProduct>();
    public CanvasGroup MoreUICanvasGroup;
    public TextMeshProUGUI MoreInfoUIText;
    
    [ContextMenu("Init")]
    public void Init()
    {
        Debug.Log("Init");
        //SO받아와서 랜덤생성
        
        ProductBtnGroup = GameObject.Find("ProductBtnGroup");
        ProductBtns = ProductBtnGroup.GetComponentsInChildren<StoreProduct>().ToList();
        MoreUICanvasGroup = MoreInfoUI.GetComponent<CanvasGroup>();
        
        for (int i = 0; i < ProductBtns.Count; i++)
        {
            ProductBtns[i].Init(StoreData.Ingredint[Random.Range(0,StoreData.Ingredint.Count)], this);
        }
    }

    //음식 저장 추가
    public void BuyIngredint()
    {
        
    }    
}
