using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerType", menuName = "Scriptable Objects/CustomerType")]

public class CustomerType : ScriptableObject
{
    public CustomerCategory customerCategory;
    public int customerHP;
    public float customerSpeed;
}
public enum CustomerCategory
{
    person,
    fatPerson,
    unkindPerson
}
