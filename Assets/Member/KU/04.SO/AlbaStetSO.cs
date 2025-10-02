using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AlbaStet", menuName = "SO/AlbaStet")]
public class AlbaStetSO : ScriptableObject
{
    public string name;
    public int age;
    public List<string> _power = new List<string>();
}
