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
    [SerializeField] private float pickDuration = 0.4f;
    
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

    // public void ItemDrop(Transform parent)
    // {
    //     int index = Random.Range(0, itemDataList.list.Count);
    //
    //     GameObject item = Instantiate(itemDataList.list[index].Prefab, parent.position, Quaternion.identity);
    //
    //     Vector3 startPos = item.transform.position;
    //
    //     // 첫 곡선 도착 지점 (땅에 떨어지는 아이템 위치)
    //     Vector3 endPos = startPos + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), 0);
    //
    //     // 베지어 Control Point
    //     Vector3 control = startPos + (endPos - startPos) * 0.5f + Vector3.up * curveHeight;
    //
    //     float t = 0;
    //
    //     DOTween.To(() => t, x =>
    //         {
    //             t = x;
    //             // ★★ 이게 핵심: item의 위치를 직접 갱신해야 움직인다!
    //             item.transform.position = CalculateBezier(startPos, control, endPos, t);
    //
    //         }, 1f, dropDuration)
    //         .SetEase(Ease.OutCubic)
    //         .OnComplete(() =>
    //         {
    //             //MoveToPlayer(item);
    //         });
    // }
    
    public void ItemDrop(Transform parent)
    {
        int index = Random.Range(0, itemDataList.list.Count);

        GameObject item = Instantiate(itemDataList.list[index].Prefab, parent.position, Quaternion.identity);

        Vector3 startPos = item.transform.position;

        // 랜덤 드랍 위치
        Vector3 endPos = startPos + new Vector3(Random.Range(-1f, -2f), Random.Range(-0.3f, 0.3f), 0);

        // 베지어 포인트를 세 개로 구성
        Vector3 control = startPos + (endPos - startPos) * 0.5f + Vector3.up * curveHeight;

        Vector3[] path =
        {
            startPos,
            control,
            endPos
        };

        item.transform.DOPath(path, dropDuration, PathType.CatmullRom)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                //MoveToPlayer(item);
            });
    }
    
    private Vector3 CalculateBezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return Mathf.Pow(1 - t, 2) * a +
               2 * (1 - t) * t * b +
               Mathf.Pow(t, 2) * c;
    }
    
    private void MoveToPlayer()
    {
        // transform.DOMove(_player.position, pickDuration)
        //     .SetEase(Ease.InQuad)
        //     .OnComplete(() =>
        //     {
        //         // 아이템을 플레이어에 전달하는 코드
        //         // 예: InventoryManager.Instance.Add(itemData);
        //         Destroy(gameObject);
        //     });
    }
}
