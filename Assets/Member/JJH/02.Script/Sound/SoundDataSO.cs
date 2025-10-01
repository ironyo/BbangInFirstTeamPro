using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "SoundSO/SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    public SoundType soundType;
    public AudioClip audioClip;
}

public enum SoundType
{
    SFX,
    BGM,
    System,
}