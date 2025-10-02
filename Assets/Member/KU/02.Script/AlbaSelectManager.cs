using UnityEngine;
using System.Collections.Generic;

public class AlbaSelectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _albaSelectMenuObj = new List<GameObject>();

    public void SelectMenuOpen()
    {
        for (int i = 0; i < _albaSelectMenuObj.Count; i++)
        {
            _albaSelectMenuObj[i].SetActive(true);
        }
    }
}
