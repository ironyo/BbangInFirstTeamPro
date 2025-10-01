using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "Scriptable Objects/SoundDataSO")]
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