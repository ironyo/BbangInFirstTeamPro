using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ItemManager : MonoSingleton<ItemManager>
{
    [field:Range(0, 100)]
    [SerializeField] private int probability;
    [SerializeField] private ItemDataListSO itemDataList;
    [SerializeField] private float curveHeight = 2.0f;
    [SerializeField] private float dropDuration = 0.7f;
    
    private float _multiplier = 1f;
    public void AddMultiplier(float value) => _multiplier += value;
    public void RemoveMultiplier(float value) => _multiplier -= value;
    
    public void TryItemDrop(Transform enemyPos)
    {
        int index = Random.Range(0, 100);
        int finalProbability = Mathf.RoundToInt(probability * _multiplier);
        
        if (index < finalProbability)
            ItemDrop(enemyPos);
    }
    
    public void ItemDrop(Transform parent)
    {
        ItemDataSO data = itemDataList.list[Random.Range(0, itemDataList.list.Count)];
        GameObject obj = Instantiate(data.DropPrefab, parent.position, Quaternion.identity);
        ItemDrop dropped = obj.GetComponent<ItemDrop>();
        
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + new Vector3(Random.Range(-1f, -2f), Random.Range(-0.3f, 0.3f), 0);
        Vector3 control = startPos + (endPos - startPos) * 0.5f + Vector3.up * curveHeight;
        
        dropped.Init(data);
        dropped.PlayDropMotion(startPos, endPos, control, dropDuration);
    }
}
