using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    [Header("Texts")]
    [SerializeField] private TMP_Text masterText;
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text sfxText;
    [SerializeField] private TMP_Text uiText;

    private bool isUpdating = false;

    private void Start()
    {
        SettingsData.Load();

        isUpdating = true;

        masterSlider.value = Snap(SettingsData.Master);
        musicSlider.value = Snap(SettingsData.Music);
        sfxSlider.value = Snap(SettingsData.SFX);
        uiSlider.value = Snap(SettingsData.UI);

        isUpdating = false;

        UpdateTexts();
    }

    public void SetMaster(float value)
    {
        if (isUpdating) return;

        Apply(ref SettingsData.Master, masterSlider, value);
    }

    public void SetMusic(float value)
    {
        if (isUpdating) return;

        Apply(ref SettingsData.Music, musicSlider, value);
    }

    public void SetSFX(float value)
    {
        if (isUpdating) return;

        Apply(ref SettingsData.SFX, sfxSlider, value);
    }

    public void SetUI(float value)
    {
        if (isUpdating) return;

        Apply(ref SettingsData.UI, uiSlider, value);
    }

    private void Apply(ref float target, Slider slider, float value)
    {
        isUpdating = true;

        float snapped = Snap(value);

        target = snapped;
        slider.value = snapped;

        UpdateTexts();
        Save();

        isUpdating = false;
    }

    private void UpdateTexts()
    {
        masterText.text = $"{Mathf.RoundToInt(masterSlider.value * 100)}%";
        musicText.text = $"{Mathf.RoundToInt(musicSlider.value * 100)}%";
        sfxText.text = $"{Mathf.RoundToInt(sfxSlider.value * 100)}%";
        uiText.text = $"{Mathf.RoundToInt(uiSlider.value * 100)}%";
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Master", SettingsData.Master);
        PlayerPrefs.SetFloat("Music", SettingsData.Music);
        PlayerPrefs.SetFloat("SFX", SettingsData.SFX);
        PlayerPrefs.SetFloat("UI", SettingsData.UI);

        PlayerPrefs.Save();
    }

    private float Snap(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }
}