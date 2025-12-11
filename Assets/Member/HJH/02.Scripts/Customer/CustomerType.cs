using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerType", menuName = "H_SO/CustomerType")]

public class CustomerType : ScriptableObject
{
    public CustomerCategory customerCategory;
    public int customerDamage;
    public int customerHP;
    public float customerSpeed;
    public int money;
}
public enum CustomerCategory
{
    person,
    fatPerson,
    unkindPerson
}
