using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ESCVolumeSetting : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject escButton;
    [SerializeField] private GameObject settingPanel;

    [Header("Slider")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    enum SettingType
    {
        Show,
        Hide
    }

    private SettingType showType = SettingType.Hide;

    private void Start()
    {
        SetVolume();
    }

    private void Update()
    {
        ESCPress();
        SetShowType();
    }

    private void SetVolume()
    {
        //슬라이더,오디오믹서 설정

        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        audioMixer.SetFloat("BGM", bgmVolume);
        bgmSlider.value = bgmVolume;

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        audioMixer.SetFloat("SFX", sfxVolume);
        audioMixer.SetFloat("System", sfxVolume);
        sfxSlider.value = sfxVolume;


    }

    private void ESCPress()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            showType = showType == SettingType.Show ? SettingType.Hide : SettingType.Show;
        }
    }

    private void SetShowType()
    {
        if (showType == SettingType.Show)
        {
            escButton.SetActive(false);
            settingPanel.SetActive(true);
        }
        else
        {
            escButton.SetActive(true);
            settingPanel.SetActive(false);
        }
    }

    #region Slider
    public void BGMSlider()
    {
        float volume = bgmSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Lerp(-80f, 10f, volume)); //오디오믹서 기준 -80~10까지 값 지정
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SFXSlider()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Lerp(-80f, 10f, volume)); //오디오믹서 기준 -80~10까지 값 지정
        audioMixer.SetFloat("System", Mathf.Lerp(-80f, 10f, volume)); //오디오믹서 기준 -80~10까지 값 지정
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("System", volume);
    }
    #endregion

    #region Button
    public void ESCButton()
    {
        showType = SettingType.Show;
    }

    public void BackButton()
    {
        showType = SettingType.Hide;
    }
    #endregion
}
