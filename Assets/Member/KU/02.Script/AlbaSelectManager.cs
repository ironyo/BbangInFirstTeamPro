using UnityEngine;
using System.Collections.Generic;

public class AlbaSelectManager : MonoBehaviour
{
    [SerializeField] private int nowPage = 1;
    [SerializeField] private List<GameObject> _albaSelectMenuObj = new();

    public void Nextpage(bool isNexBt)
    {
        if (isNexBt && nowPage != 3)
        {
            nowPage++;
        }
        else if (nowPage != 1)
        {
            nowPage--;
        }
    }
}
