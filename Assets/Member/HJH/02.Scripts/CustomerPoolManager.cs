using UnityEngine;

public class CustomerPoolManager : MonoBehaviour
{
    [SerializeField] private CustomerTypeSOList customerTypeSOList;
    [SerializeField] private GameObject customerPrefab;
    public Customer Get()
    {
        GameObject go = Instantiate(customerPrefab);
        return go.GetComponent<Customer>();
    }

    public (GuestType, string) GetRandomCustomerData()
    {
        if (customerTypeSOList.customerTypeSOList.Length == 0)
            return (GuestType.None, null);

        int typeIndex = Random.Range(0, customerTypeSOList.customerTypeSOList.Length);
        CustomerTypeSO chosenType = customerTypeSOList.customerTypeSOList[typeIndex];

        if (chosenType.customerName.Length == 0)
            return (chosenType.guestType, null);

        int nameIndex = Random.Range(0, chosenType.customerName.Length);
        string chosenName = chosenType.customerName[nameIndex];

        return (chosenType.guestType, chosenName);
    }
}
