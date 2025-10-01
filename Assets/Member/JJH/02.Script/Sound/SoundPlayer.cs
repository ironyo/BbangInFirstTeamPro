using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup bgmGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixerGroup systemGroup;
    private AudioSource audioSource;

    public async void PlaySound(SoundDataSO soundData)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundData.audioClip;

        switch (soundData.soundType)
        {
            case SoundType.BGM: //bgm�̸� ����
                audioSource.loop = true;
                audioSource.ignoreListenerPause = false;
                audioSource.outputAudioMixerGroup = bgmGroup;
                break;
            case SoundType.SFX: //sfx
                audioSource.ignoreListenerPause = false;
                audioSource.outputAudioMixerGroup = sfxGroup;
                break;
            case SoundType.System: //�ý����̸� ���� ����
                audioSource.ignoreListenerPause = true;
                audioSource.outputAudioMixerGroup = systemGroup;
                break;
        }
        audioSource.Play();


        await Awaitable.WaitForSecondsAsync(10); //10�� ��ٸ���

        if (this == null)
            return;

        if (soundData.soundType != SoundType.BGM) //BGM�� �ƴϸ� ���� �ı�
            Destroy(gameObject);
    }
}
