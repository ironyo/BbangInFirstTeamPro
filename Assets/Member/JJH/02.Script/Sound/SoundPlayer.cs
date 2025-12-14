using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup bgmGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixerGroup systemGroup;
    private AudioSource audioSource;

    public void PlaySound(SoundDataSO soundData)
    {
        if (soundData.soundType == SoundType.BGM)
            RemoveExistingBgmPlayers();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundData.audioClip;

        switch (soundData.soundType)
        {
            case SoundType.BGM: //bgm이면 루프
                audioSource.loop = true;
                audioSource.ignoreListenerPause = false;
                audioSource.outputAudioMixerGroup = bgmGroup;
                break;
            case SoundType.SFX: //sfx
                audioSource.ignoreListenerPause = false;
                audioSource.outputAudioMixerGroup = sfxGroup;
                break;
            case SoundType.System: //시스템이면 멈춤 무시
                audioSource.ignoreListenerPause = true;
                audioSource.outputAudioMixerGroup = systemGroup;
                break;
        }
        audioSource.Play();

        if (soundData.soundType != SoundType.BGM) //BGM이 아니면 사운드 파괴
            StartCoroutine(DestroySound(audioSource.clip.length));
    }

    private void RemoveExistingBgmPlayers()
    {
        SoundPlayer[] soundPlayers = Object.FindObjectsByType<SoundPlayer>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (SoundPlayer p in soundPlayers)
        {
            if (p == this)
                continue;

            AudioSource src = p.GetComponent<AudioSource>();
            if (src == null)
                continue;

            bool isBgm = src.loop && src.outputAudioMixerGroup == bgmGroup && src.isPlaying;

            if (isBgm)
                Destroy(p.gameObject);
        }
    }


    private IEnumerator DestroySound(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
