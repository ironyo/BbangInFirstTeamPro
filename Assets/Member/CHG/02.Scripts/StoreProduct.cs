using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreProduct : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameText; //재료 이름
    [SerializeField] private TextMeshProUGUI PriceText; 
    [SerializeField] private TextMeshProUGUI StockText; 

    [SerializeField] private Image IngredintImage; //재료 Sprite표시 Image
    [SerializeField] private Image MoreInfomationBtn; //추가 정보창 버튼

    private IngredientSO _ingredientData; //재료 Data
    private StoreManager _storeManager;
    private int _stock; //한번에 판매하는 재료 개수
    private int _price; //가격
    private Button btn;
    
    public void Init(IngredientSO ingredientData, StoreManager storeManager)
    {
        if (ingredientData == null && storeManager == null) return;
        btn = GetComponent<Button>();
        this._storeManager = storeManager;
        this._ingredientData = ingredientData;
        IngredintImage.sprite = _ingredientData.FoodSprite;
        NameText.text = _ingredientData.foodName;
        _stock = _ingredientData.Stock;
        StockText.text = $"재료 개수: {_stock}";
        _price = _ingredientData.Price;
        PriceText.text = $"가격: {_price}";
    }

    public void MoreInfoBtnEnter(RectTransform pos)
    {
        Debug.Log("진입");
        _storeManager.MoreUICanvasGroup.alpha = 1;
        _storeManager.MoreInfoUIText.text = $"종류: {_ingredientData.foodGroup} \n 맛: {_ingredientData.foodTaste} \n " +
                                            $"식감: {_ingredientData.foodTextureType} \n 등급: {_ingredientData.foodRarityType}";
        _storeManager.MoreInfoUI.rectTransform.position = pos.position;
    }

    public void MoreInfoBtnExit()
    {
        _storeManager.MoreUICanvasGroup.alpha = 0;
    }

    public void BuyButtonClick()
    {
        
        if (MoneyManager.Instance.Money < _ingredientData.Price)
        {
            btn.interactable = false;
            return;
        }

        MoneyManager.Instance.Money -= _ingredientData.Price;
        _stock--;
        StockText.text = $"재료 개수: {_stock}";

        if (_stock <= 0)
        {
            btn.interactable = false;
        }

        _storeManager.BuyIngredint();
    }
}
