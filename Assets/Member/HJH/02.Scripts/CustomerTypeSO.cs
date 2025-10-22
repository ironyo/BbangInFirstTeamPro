using UnityEngine;
public enum GuestType
{
    customer, // 일반손님
    ObnoxiousCustomer, // 진상
    Rich, // 부자손님
    Celebrity, // 연예인
    Alien // 외계인
}

[CreateAssetMenu(fileName = "CustomerTypeSO", menuName = "H_SO/CustomerTypeSO")]

public class CustomerTypeSO : ScriptableObject
{
    public GuestType guestType;
    public string[] customerName;
    public Color typeColor;
}