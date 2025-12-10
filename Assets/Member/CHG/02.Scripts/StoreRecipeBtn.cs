using UnityEngine;

public class StoreRecipeBtn : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI NameText;
    //[SerializeField] private TextMeshProUGUI PriceText;
    //[SerializeField] private Image Image;
    //private RecipeSO _recipeData;
    //private StoreManager_TJa _storeManager;
    //private int _price;
    //private StringBuilder _sb;
    //public void Init(RecipeSO recipeData, StoreManager_TJa storeManager)
    //{
    //    if (recipeData == null || storeManager == null) return;

    //    _recipeData = recipeData;
    //    _storeManager = storeManager;

    //    _price = _recipeData.Price;
    //    Image.sprite = recipeData.recipeImage;

    //    TextSet();
    //}

    //private void TextSet()
    //{
    //    NameText.text = _recipeData.foodName;
    //    PriceText.text = $"가격: {_price}";

    //}
    //#region MoreInfo
    //public void MoreInfoBtnEnter(RectTransform pos)
    //{

    //    //_storeManager.MoreUICanvasGroup.alpha = 1;

    //    _sb.Clear();
    //    _sb.AppendFormat($"");
    //    //    foreach (var item in _recipeData.foodTaste)
    //    //{
    //    //    _sb.AppendFormat($"맛: {item}\n");
    //    //}

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

    //    MoneyManager.Instance.SpendMoney(_recipeData.Price);


    //    //_stock--;
    //    //StockText.text = $"재료 개수: {_stock}";
    //    _storeManager.BuyIngredint();
    //    CanBuyCheck(); // 구매하고 다시 확인
    //}


    ////남은 상품수와 보유중인 돈이 가격보다 많은지 확인
    //private bool CanBuyCheck()
    //{
    //    var moneyManager = MoneyManager.Instance;
    //    Debug.Assert(moneyManager != null, "MoneyManager is null");

    //    // 돈이 부족하거나 재고가 없으면 구매 불가능
    //    //if (moneyManager.Money < _recipeData.Price || _stock <= 0)
    //    //{
    //    //    OnWorning?.Invoke("구매가 불가능합니다");

    //    //    return true; // 구매 불가능
    //    //}

    //    return false; // 구매 가능
    //}

}




