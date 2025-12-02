using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Person : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected string personName;
    [SerializeField] protected string description;

    protected abstract void Clicked();

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked();
        Debug.Log("나는 " +  name + "이고, 나의 기능은 다음과 같아. \n " + description);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(personName + "에 마우스를 올림");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(personName + "에 마우스를 내림");
    }
}
