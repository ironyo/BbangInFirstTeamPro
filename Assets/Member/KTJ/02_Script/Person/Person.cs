using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Person : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Setting")]
    [SerializeField] protected TextMeshPro nameTxt;
    [SerializeField] protected string personName;
    [SerializeField] protected string description;

    [Header("ClickedUI")]
    [SerializeField] protected GameObject _clickedUI;

    private bool _isClicked = false;

    private void Awake()
    {
        nameTxt.text = personName;
    }

    public virtual void Clicked()
    {
        CameraEffectManager.Instance.CameraMoveTarget(gameObject);
        CameraEffectManager.Instance.CameraZoom(2f, 1f);
        _clickedUI.SetActive(true);

        _isClicked = true;
    }

    public virtual void UnClicked()
    {
        CameraEffectManager.Instance.CameraMoveTarget(CameraEffectManager.Instance.CameraTarget.gameObject);
        //CameraEffectManager.Instance.CameraZoom(5f, 0.1f);
        Debug.Log("카메라줌!!!!!!!!!!! => 5");
        _clickedUI.SetActive(false);

        _isClicked = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (StageManager.Instance.IsRunning) return;
        //Clicked();
        //_isClicked = true;
        //Debug.Log("나는 " +  name + "이고, 나의 기능은 다음과 같아. \n " + description);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(personName + "에 마우스를 올림");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(personName + "에 마우스를 내림");
    }
}
