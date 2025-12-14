using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ESCVolumeSetting : MonoBehaviour
{
    [Header("InGame?")]
    [SerializeField] private bool inGame = false;

    [Header("GameObject")]
    [SerializeField] private GameObject escButton;
    [SerializeField] private CanvasGroup settingPanel;

    [Header("Slider")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    private RectTransform panelRect;

    enum SettingType
    {
        Show,
        Hide
    }

    private SettingType showType = SettingType.Hide;

    private void Start()
    {
        panelRect = settingPanel.GetComponent<RectTransform>();
        SetVolume();
    }

    private void Update()
    {
        ESCPress();
    }

    private void SetVolume()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        SetBGMToMixer(bgmVolume);
        SetSFXToMixer(sfxVolume);
    }

    private void ESCPress()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && inGame)
        {
            showType = showType == SettingType.Show ? SettingType.Hide : SettingType.Show;
            SetShowType();
        }
    }

    private void SetShowType()
    {
        if (showType == SettingType.Show)
        {
            escButton.SetActive(false);
            settingPanel.interactable = true;
            settingPanel.blocksRaycasts = true;
            settingPanel.DOFade(1, 0.3f).SetUpdate(true);
            panelRect.DOAnchorPos(new Vector3(0, 0, 0), 0.3f).SetUpdate(true);
            Time.timeScale = 0f;
        }
        else
        {
            escButton.SetActive(true);
            panelRect.DOAnchorPos(new Vector3(0, 1200, 0), 0.3f).SetUpdate(true)
                .OnComplete(() =>
                {
                    settingPanel.interactable = false;
                    settingPanel.blocksRaycasts = false;
                    settingPanel.DOFade(0, 0.3f).SetUpdate(true);
                });
            Time.timeScale = 1f;
        }
    }

    private void SetBGMToMixer(float volume)
    {
        if (volume <= 0.0001f)
            volume = -80f;
        else
            volume = Mathf.Log10(volume) * 20f;
        audioMixer.SetFloat("BGM", volume);
    }

    private void SetSFXToMixer(float volume)
    {
        if (volume <= 0.0001f)
            volume = -80f;
        else
            volume = Mathf.Log10(volume) * 20f;
        audioMixer.SetFloat("SFX", volume);
        audioMixer.SetFloat("System", volume);
    }

    #region Slider
    public void BGMSlider()
    {
        float volume = bgmSlider.value;
        SetBGMToMixer(volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SFXSlider()
    {
        float volume = sfxSlider.value; // 0~1
        SetSFXToMixer(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
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

    public void LobbyButton()
    {
        //씬 이동(메인 화면으로 나가기)
    }
    #endregion
}
