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

    enum ShowType
    {
        Show,
        Hide
    }

    private ShowType showType = ShowType.Hide;

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
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        bgmSlider.value = bgmVolume;
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = sfxVolume;
    }

    private void ESCPress()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            showType = showType == ShowType.Show ? ShowType.Hide : ShowType.Show;
        }
    }

    private void SetShowType()
    {
        if (showType == ShowType.Show)
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
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SFXSlider()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("System", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    #endregion

    #region Button
    public void ESCButton()
    {
        showType = ShowType.Show;
    }

    public void BackButton()
    {
        showType = ShowType.Hide;
    }
    #endregion
}
