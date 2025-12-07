using UnityEngine;

[System.Serializable]
public class CustomerTypeEntry
{
    public CustomerType type;
    public float weight;
}

[CreateAssetMenu(fileName = "CustomerTypeList", menuName = "Scriptable Objects/CustomerTypeList")]
public class CustomerTypeList : ScriptableObject
{
    public CustomerTypeEntry[] customerTypes;
}

