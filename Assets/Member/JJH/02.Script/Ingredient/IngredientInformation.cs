using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject panel;
    private TextMeshProUGUI text;

    private IngredientSO information;
    private int foodAmount = 1;


    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        panel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
        text.text =
            $"{information.foodName}\n" +
            $"{information.foodGroup}\n" +
            $"{information.foodTaste}\n" +
            $"{information.foodTextureType}\n" +
            $"{information.foodRarityType}\n" +
            $"{foodAmount}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }

    public void Create(IngredientSO ingredient, int amount)
    {
        information = ingredient;
        foodAmount = amount;
    }
}
