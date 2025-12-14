using UnityEngine;

public class BGMManager : MonoSingleton<BGMManager>
{
    [SerializeField] SoundDataSO _mainData;
    [SerializeField] SoundDataSO _fightData;

    private void Start()
    {
        SoundManager.Instance.PlaySound(_mainData);
    }
    public void MainSound()
    {
        SoundManager.Instance.PlaySound(_mainData);
    }
    public void FightSound()
    {
        SoundManager.Instance.PlaySound(_fightData);
    }
}
