using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreativeCustomer : MonoBehaviour
{
    [SerializeField] private CustomerPoolManager poolManager;
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            CreateCustomer();
        }
    }

    void CreateCustomer()
    {
        Customer customer = poolManager.Get();
        if (customer == null)
        {
            return;
        }

        customer.transform.position = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        customer.gameObject.SetActive(true);

        (GuestType, string) type = poolManager.GetRandomCustomerData();
        customer.Init(type.Item1, type.Item2);
    }
}
