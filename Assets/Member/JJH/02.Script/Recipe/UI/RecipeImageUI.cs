using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeImageUI : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image image;
    private GameObject describeObject;
    private Outline outline;

    private RecipeSO information;

    public void Create(RecipeSO ingredient)
    {
        outline = GetComponent<Outline>();

        outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);

    }

    public void OnSelect(BaseEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outline.enabled = false;
    }
}
