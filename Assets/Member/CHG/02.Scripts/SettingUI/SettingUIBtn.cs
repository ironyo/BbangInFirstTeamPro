using DG.Tweening;
using UnityEngine;

public class SettingUIBtn : MonoBehaviour
{
    [SerializeField] private GameObject _realExitUI;

    public void ExitGame()
    {
        _realExitUI.transform.DOScale(1, 0.4f);
    }

    public void CloseRealExit()
    {
        _realExitUI.transform.DOScale(0, 0.4f);
    }
    public void RealExitGame()
    {
        Application.Quit();
    }
    public void CloseSetting()
    {

    }
}
