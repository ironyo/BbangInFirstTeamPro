using UnityEngine;

public class AlbaManager : MonoBehaviour
{
    [SerializeField] private GameObject plusGroup;
    [SerializeField] private GameObject minusGroup;
    [SerializeField] private GameObject menuGroup;
    public void OpenMenu(bool isPlus)
    {
        menuGroup.SetActive(false);
        if (isPlus)
        {
            plusGroup.SetActive(true);
            minusGroup.SetActive(false);
        }
        else
        {
            plusGroup.SetActive(false);
            minusGroup.SetActive(true);
        }
    }
    public void CloseAllMenu()
    {
        plusGroup.SetActive(false);
        minusGroup.SetActive(false);
        menuGroup.SetActive(true);
    }

    public void GetxAlbaData()
    {

    }
}
