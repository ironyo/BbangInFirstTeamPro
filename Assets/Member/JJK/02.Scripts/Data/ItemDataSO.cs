using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "JJK_SO/ItemSO")]
public class ItemDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public GameObject Prefab { get; set; }
    [field: SerializeField] public float Value { get; set; }
    [field: SerializeField] public float Duration { get; set; }
}
