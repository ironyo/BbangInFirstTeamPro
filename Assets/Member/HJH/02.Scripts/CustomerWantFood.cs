using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWantFood : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private TextMeshPro foodName;

    public WantRecipeSOList wantRecipeSOList;
    private RecipeListSO customerRecipeSOList;
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

        r = customerRecipeSOList.recipeList[Random.Range(0, customerRecipeSOList.recipeList.Length)];

        foodImage.sprite = r.recipeImage;
        foodName.text = r.foodName;
    }
}
