using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private StatBars statBars;
    [SerializeField] private UI ui;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerStats stats;

    private float currentXP = 0f;
    public int Level { get; private set; }

    private bool isChoosingUpgrade = false;

    private float XpToNextLevel => 10 + (Level - 1) * 5;

    private void Start()
    {
        Level = RunData.Instance.playerLevel;
        currentXP = RunData.Instance.currentXP;

        ui.UpdateLevelText(Level);
        statBars.SetXPInstant(currentXP / XpToNextLevel);
    }

    public void AddXP(float amount)
    {
        currentXP += amount * (1f + stats.ExperienceMultiplier);

        if (isChoosingUpgrade)
            return;

        if (currentXP >= XpToNextLevel)
        {
            statBars.SetXP(1f);
            LevelUp();
            return;
        }

        statBars.SetXP(currentXP / XpToNextLevel);
        SaveProgress();
    }

    private void LevelUp()
    {
        soundManager.PlayLevelUp();
        soundManager.PlayCardsReveal();

        currentXP -= XpToNextLevel;

        Level++;

        SaveProgress();

        ui.UpdateLevelText(Level);

        isChoosingUpgrade = true;

        upgradeManager.ShowUpgrades();
    }

    public void FinishUpgradeSelection()
    {
        isChoosingUpgrade = false;

        if (currentXP >= XpToNextLevel)
        {
            statBars.SetXP(1f);
            LevelUp();
            return;
        }

        RefreshXPBar();
    }

    public void RefreshXPBar()
    {
        statBars.SetXP(currentXP / XpToNextLevel);
    }

    private void SaveProgress()
    {
        RunData.Instance.playerLevel = Level;
        RunData.Instance.currentXP = currentXP;
    }
}