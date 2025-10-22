using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreativeCustomer : MonoBehaviour
{
    [SerializeField] private CustomerPoolManager poolManager;
    [SerializeField] private Transform spawnPoint;

    [Header("Spawn Area Settings")]
    [SerializeField] private float innerRadius = 2f;
    [SerializeField] private float outerRadius = 5f;

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            CreateCustomer();
        }
    }

    void CreateCustomer()
    {
        Customer customer = poolManager.Get();
        if (customer == null) return;

        Vector2 randomPos = GetRandomPositionInRing(spawnPoint.position, innerRadius, outerRadius);
        customer.transform.position = randomPos;

        customer.gameObject.SetActive(true);

        (GuestType, string) type = poolManager.GetRandomCustomerData();
        customer.Init(type.Item1, type.Item2);
    }

    Vector2 GetRandomPositionInRing(Vector2 center, float innerRadius, float outerRadius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Mathf.Sqrt(Random.Range(innerRadius * innerRadius, outerRadius * outerRadius));
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        return center + offset;
    }

#if UNITY_EDITOR
    // 손님 생성 범위 Gizmo 표시
    private void OnDrawGizmosSelected()
    {
        if (spawnPoint == null) return;

        Handles.color = new Color(0, 0, 1, 0.1f);
        Handles.DrawSolidDisc(spawnPoint.position, Vector3.forward, outerRadius);
        Handles.color = new Color(1, 1, 1, 1f);
        Handles.DrawWireDisc(spawnPoint.position, Vector3.forward, innerRadius);
    }
#endif
}
