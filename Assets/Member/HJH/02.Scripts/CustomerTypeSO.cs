using UnityEngine;
public enum GuestType
{
    None,
    customer,
    ObnoxiousCustomer,
    Rich,
    Celebrity,
    Alien
}

[CreateAssetMenu(fileName = "CustomerTypeSO", menuName = "SO/CustomerTypeSO")]

public class CustomerTypeSO : ScriptableObject
{
    public GuestType guestType;
    public string customerName;
}
