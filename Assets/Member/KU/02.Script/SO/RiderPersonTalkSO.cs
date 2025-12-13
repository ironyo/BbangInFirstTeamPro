using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RiderPersonTalkSO", menuName = "SO/RiderPersonTalkSO")]
public class RiderPersonTalkSO : ScriptableObject
{
    public List<string> messageList;
}
