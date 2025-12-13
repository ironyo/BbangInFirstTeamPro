using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StageChannelInt", menuName = "H_SO/StageChannelInt")]
public class StageChannelInt : ScriptableObject
{
    public event Action<int> OnEventRaised;

    [SerializeField]
    private int stageNum = 0;

    public void RaiseEvent()
    {
        stageNum++;
        OnEventRaised?.Invoke(stageNum);
    }

    public int CurrentStage => stageNum;
}
