using UnityEngine;

public class StoreProductBtn : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI NameText; //재료 이름
    //[SerializeField] private TextMeshProUGUI PriceText;
    //[SerializeField] private TextMeshProUGUI StockText;

    //[SerializeField] private Image IngredintImage; //재료 Sprite표시 Image

    //private IngredientSO _ingredientData; //재료 Data
    //private StoreManager_TJa _storeManager;
    //private int _stock; //한번에 판매하는 재료 개수
    //private int _price; //가격
    //private StringBuilder _sb = new();

    //public UnityEvent<string> OnWorningMessage;
    //public UnityEvent<StringBuilder, RectTransform> InfoIShow;
    //public UnityEvent InfoUiHide;
    //public void Init(IngredientSO ingredientData, StoreManager_TJa storeManager)
    //{
    //    if (ingredientData == null || storeManager == null) return;


    //    this._storeManager = storeManager;
    //    this._ingredientData = ingredientData;
    //    IngredintImage.sprite = _ingredientData.FoodSprite;

    //    _stock = _ingredientData.Stock;
    //    _price = _ingredientData.Price;

    //    TextSet();
    //}

    //private void TextSet()
    //{
    //    NameText.text = _ingredientData.foodName;
    //    StockText.text = $"재료 개수: {_stock}";
    //    PriceText.text = $"가격: {_price}";

    //}
    //#region MoreInfo
    //public void MoreInfoBtnEnter(RectTransform pos)
    //{

    //    //_storeManager.MoreUICanvasGroup.alpha = 1;

    //    _sb.Clear();
    //    _sb.AppendFormat($"종류: {_ingredientData.foodGroup}\n");
    //    _sb.AppendFormat($"식감: {_ingredientData.foodTextureType}\n");
    //    _sb.AppendFormat($"등급: {_ingredientData.foodRarityType}\n");
    //    foreach (var item in _ingredientData.foodTaste)
    //    {
    //        _sb.AppendFormat($"맛: {item}\n");
    //    }

    //    //_storeManager.MoreInfoUIText.text = _sb.ToString();

    //    //_storeManager.MoreInfoUI.rectTransform.position = pos.position;
    //}

    //public void MoreInfoBtnExit()
    //{
    //    //_storeManager.MoreUICanvasGroup.alpha = 0;
    //}

    //#endregion



    ////구매버튼 클릭
    //public void BuyButtonClick()
    //{
    //    //조건 확인
    //    if (CanBuyCheck()) return;

    //    MoneyManager.Instance.SpendMoney(_ingredientData.Price);


    //    _stock--;
    //    StockText.text = $"재료 개수: {_stock}";
    //    _storeManager.BuyIngredint();
    //    CanBuyCheck(); // 구매하고 다시 확인
    //}


    ////남은 상품수와 보유중인 돈이 가격보다 많은지 확인
    //private bool CanBuyCheck()
    //{
    //    var moneyManager = MoneyManager.Instance;
    //    Debug.Assert(moneyManager != null, "MoneyManager is null");

    //    // 돈이 부족하거나 재고가 없으면 구매 불가능
    //    if (moneyManager.Money < _ingredientData.Price || _stock <= 0)
    //    {
    //        //OnWorning?.Invoke("구매가 불가능합니다");

    //        return true; // 구매 불가능
    //    }

    //    return false; // 구매 가능
    //}

}
