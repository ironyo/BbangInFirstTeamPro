using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Mechanic : Person
{
    [SerializeField] private Button _readyBtn;

    public Button GetReadyBtn()
    {
        return _readyBtn;
    }
}
