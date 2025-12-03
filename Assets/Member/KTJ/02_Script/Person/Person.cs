using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Person : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected TextMeshPro nameTxt;
    [SerializeField] protected string personName;
    [SerializeField] protected string description;

    private bool _isClicked = false;

    private void Awake()
    {
        nameTxt.text = personName;
    }

    private void Update()
    {
        if (_isClicked == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnClicked();
            }
        }
    }

    protected abstract void Clicked();

    protected abstract void UnClicked();

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked();
        _isClicked = true;
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
