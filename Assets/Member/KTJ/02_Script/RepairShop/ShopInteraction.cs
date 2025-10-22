using System;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    [SerializeField] private RectTransform RepairShop;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 누름");
            RepairShopEnter();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R 누름");
            RepairShopExit();
        }
    }
    private void RepairShopEnter()
    {
        Debug.Log("정비소 입장");
        //RepairShop.gameObject.GetComponent<CanvasGroup>()
    }

    private void RepairShopExit()
    {
        Debug.Log("정비소 퇴장");
    }

}
