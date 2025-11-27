using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HaveItemUI : MonoBehaviour
{
    private Dictionary<IngredientSO, TextMeshProUGUI> HaveInGredientUI = new Dictionary<IngredientSO, TextMeshProUGUI>();
    private List<IngredientSO> TAllIngredient;
    [SerializeField] private GameObject UIPrefab;
    private void Awake()
    {
        foreach (var item in TAllIngredient)
        {
            GameObject obj = Instantiate(UIPrefab, gameObject.transform);
            obj.GetComponentInChildren<Image>().sprite = item.FoodSprite;
            HaveInGredientUI.Add(item, obj.GetComponent<TextMeshProUGUI>());
            obj.SetActive(false);

        }
    }
    private void Start()
    {
        IngredientInventoryManager.Instance.OnInventoryChanged += IngredientInventoryChange;
    }

    private void IngredientInventoryChange(IngredientSO sO, int arg2)
    {
        throw new NotImplementedException();
    }
}
