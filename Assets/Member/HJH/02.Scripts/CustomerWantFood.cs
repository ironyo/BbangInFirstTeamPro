using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWantFood : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private TextMeshPro foodName;

    public WantRecipeSOList wantRecipeSOList;
    private RecipeSOList customerRecipeSOList;
    private RecipeSO r;

    private Customer customer;

    private void Awake()
    {
        customer = GetComponent<Customer>();
    }

    private void Start()
    {
        int index = (int)customer.guestType;
        customerRecipeSOList = wantRecipeSOList.recipeSOLists[index];

        r = customerRecipeSOList.recipeSOList[Random.Range(0, customerRecipeSOList.recipeSOList.Length)];

        foodImage.sprite = r.recipeImage;
        foodName.text = r.foodName;
    }
}
