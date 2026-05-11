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

    [Header("Nickname")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text currentNameText;
    [SerializeField] private TMP_Text nameMessage;
    [SerializeField] private Button applyButton;
    [SerializeField] private LeaderboardService leaderboardService;

    [SerializeField] private TMP_Dropdown languageDropdown;

    [Header("Difficulty")]
    [SerializeField] private GameObject difficultyPanel;
    [SerializeField] private GameObject wandererSelected;
    [SerializeField] private GameObject lastKnightSelected;

    private bool isUpdating = false;
    private bool destroyed = false;

    private void OnDestroy()
    {
        destroyed = true;
    }

    private void Start()
    {
        SettingsData.Load();
        isUpdating = true;
        masterSlider.value = Snap(SettingsData.Master);
        musicSlider.value = Snap(SettingsData.Music);
        sfxSlider.value = Snap(SettingsData.SFX);
        uiSlider.value = Snap(SettingsData.UI);
        languageDropdown.value = (int)SettingsData.Language;
        isUpdating = false;
        UpdateTexts();
        PlayerProfile.Load();
        RefreshLocalizedTexts();
        if (nameInput != null)
            nameInput.characterLimit = 16;
        RefreshDifficultyUI();
    }

    public void SetMaster(float value)
    {
        if (isUpdating)
            return;
        Apply(ref SettingsData.Master, masterSlider, value);
    }

    public void SetMusic(float value)
    {
        if (isUpdating)
            return;
        Apply(ref SettingsData.Music, musicSlider, value);
    }

    public void SetSFX(float value)
    {
        if (isUpdating)
            return;
        Apply(ref SettingsData.SFX, sfxSlider, value);
    }

    public void SetUI(float value)
    {
        if (isUpdating)
            return;
        Apply(ref SettingsData.UI, uiSlider, value);
    }

    public void SetLanguage(int index)
    {
        SettingsData.Language = (Language)index;
        SettingsData.Save();
        LocalizationManager.Instance.RefreshAll();
        RefreshLocalizedTexts();
    }

    public async void ApplyName()
    {
        string input = nameInput.text;
        if (!NicknameValidator.TryValidate(input, out string errorKey, out string clean))
        {
            nameMessage.text = LocalizationManager.Instance.GetText(errorKey);
            return;
        }
        if (clean == PlayerProfile.PlayerName)
        {
            nameMessage.text = LocalizationManager.Instance.GetText("settings_name_same");
            return;
        }
        PlayerProfile.SetName(clean);
        currentNameText.text = $"{LocalizationManager.Instance.GetText("settings_current_name")} {clean}";
        nameMessage.text = LocalizationManager.Instance.GetText("settings_name_saved");
        int bestScore = PlayerProfile.BestScore;
        if (bestScore <= 0 || leaderboardService == null)
            return;
        if (applyButton != null)
            applyButton.interactable = false;
        try
        {
            await leaderboardService.SubmitScoreAsync(bestScore);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SettingsMenu] Failed to submit score: {e}");
        }
        finally
        {
            if (!destroyed && applyButton != null)
                applyButton.interactable = true;
        }
    }

    private void RefreshLocalizedTexts()
    {
        if (currentNameText != null)
            currentNameText.text = $"{LocalizationManager.Instance.GetText("settings_current_name")} {PlayerProfile.PlayerName}";
        if (nameMessage != null)
            nameMessage.text = LocalizationManager.Instance.GetText("settings_name_default_message");
    }

    private void Apply(ref float target, Slider slider, float value)
    {
        isUpdating = true;
        float snapped = Snap(value);
        target = snapped;
        slider.value = snapped;
        UpdateTexts();
        SettingsData.Save();
        isUpdating = false;
    }

    private void UpdateTexts()
    {
        masterText.text = $"{Mathf.RoundToInt(masterSlider.value * 100)}%";
        musicText.text = $"{Mathf.RoundToInt(musicSlider.value * 100)}%";
        sfxText.text = $"{Mathf.RoundToInt(sfxSlider.value * 100)}%";
        uiText.text = $"{Mathf.RoundToInt(uiSlider.value * 100)}%";
    }

    private float Snap(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }

    public void OpenDifficultyPanel()
    {
        if (difficultyPanel == null)
            return;
        difficultyPanel.SetActive(true);
        RefreshDifficultyUI();
    }

    public void CloseDifficultyPanel()
    {
        if (difficultyPanel == null)
            return;
        difficultyPanel.SetActive(false);
    }

    public void SelectWanderer()
    {
        DifficultyManager.CurrentDifficulty = Difficulty.Wanderer;
        RefreshDifficultyUI();
        CloseDifficultyPanel();
    }

    public void SelectLastKnight()
    {
        DifficultyManager.CurrentDifficulty = Difficulty.LastKnight;
        RefreshDifficultyUI();
        CloseDifficultyPanel();
    }

    private void RefreshDifficultyUI()
    {
        bool wanderer = DifficultyManager.CurrentDifficulty == Difficulty.Wanderer;
        if (wandererSelected != null)
            wandererSelected.SetActive(wanderer);
        if (lastKnightSelected != null)
            lastKnightSelected.SetActive(!wanderer);
    }
}