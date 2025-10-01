public class SoundManager : MonoSingleton<SoundManager>
{
    public SoundPlayer soundPlayerPrefab;

    public void PlaySound(SoundDataSO soundData)
    {
        SoundPlayer soundPlayer = Instantiate(soundPlayerPrefab, transform);
        soundPlayer.PlaySound(soundData);
    }
}
