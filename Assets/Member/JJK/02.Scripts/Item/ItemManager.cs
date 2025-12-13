using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class ItemManager : MonoSingleton<ItemManager>
{
    [field:Range(0, 100)]
    [SerializeField] private int probability;
    [SerializeField] private ItemDataListSO itemDataList;
    [SerializeField] private float curveHeight = 2.0f;
    [SerializeField] private float dropDuration = 0.7f;
    
    public void TryItemDrop(Transform enemyPos)
    {
        int index = Random.Range(0, 100);
        
        if (index < probability)
            ItemDrop(enemyPos);
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
            ItemDrop(transform);
    }
    
    public void ItemDrop(Transform parent)
    {
        ItemDataSO data = itemDataList.list[Random.Range(0, itemDataList.list.Count)];
        GameObject obj = Instantiate(data.Prefab, parent.position, Quaternion.identity);
        ItemDrop dropped = obj.GetComponent<ItemDrop>();

        // 3) Bezier 포인트 준비
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + new Vector3(Random.Range(-1f, -2f), Random.Range(-0.3f, 0.3f), 0);
        Vector3 control = startPos + (endPos - startPos) * 0.5f + Vector3.up * curveHeight;

        // 4) 드랍된 아이템 초기화 + 모션 실행
        dropped.Init(data);
        dropped.PlayDropMotion(startPos, endPos, control, dropDuration);
    }
}
