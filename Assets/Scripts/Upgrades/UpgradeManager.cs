using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CardUI[] cards;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private float inputBlockTime = 0.6f;

    private float inputBlockTimer = 0f;
    private bool isBlockingInput = false;

    [Header("Card Sprites")]
    [SerializeField] private Sprite damageMultiplierCard;
    [SerializeField] private Sprite swordDamageCard;
    [SerializeField] private Sprite swordRangeCard;
    [SerializeField] private Sprite swordKnockback;
    [SerializeField] private Sprite speedCard;
    [SerializeField] private Sprite maxHealthCard;
    [SerializeField] private Sprite regenCard;
    [SerializeField] private Sprite experienceCard;
    [SerializeField] private Sprite daggerUnlockCard;
    [SerializeField] private Sprite daggerDamageCard;
    [SerializeField] private Sprite daggerCountCard;
    [SerializeField] private Sprite daggerPenetrationCard;
    [SerializeField] private Sprite scoreMultiplierCard;

    private List<Upgrade> upgrades = new List<Upgrade>();

    public bool IsChoosingUpgrade => upgradePanel.activeSelf;

    private void Awake()
    {
        upgrades.Add(new DamageMultiplierUpgrade(damageMultiplierCard));
        upgrades.Add(new SwordDamageUpgrade(swordDamageCard));
        upgrades.Add(new SwordRangeUpgrade(swordRangeCard));
        upgrades.Add(new SwordKnockbackUpgrade(swordKnockback));
        upgrades.Add(new SpeedUpgrade(speedCard));
        upgrades.Add(new MaxHealthUpgrade(maxHealthCard));
        upgrades.Add(new HealthRegenUpgrade(regenCard));
        upgrades.Add(new ExperienceUpgrade(experienceCard));
        upgrades.Add(new DaggerUnlockUpgrade(daggerUnlockCard));
        upgrades.Add(new DaggerDamageUpgrade(daggerDamageCard));
        upgrades.Add(new DaggerCountUpgrade(daggerCountCard));
        upgrades.Add(new DaggerPenetrationUpgrade(daggerPenetrationCard));
        upgrades.Add(new ScoreMultiplierUpgrade(scoreMultiplierCard));

        LoadUpgradeLevels();
        ApplyLoadedUpgrades();
    }

    private void Update()
    {
        if (!isBlockingInput)
            return;

        inputBlockTimer -= Time.unscaledDeltaTime;

        if (inputBlockTimer <= 0f)
        {
            isBlockingInput = false;
            inputBlocker.SetActive(false);
        }
    }

    public void ShowUpgrades()
    {
        Time.timeScale = 0f;
        CursorManager.ShowCursor();
        playerInput.DisableInput();
        List<Upgrade> selectedUpgrades = GetRandomUpgrades(3);

        for (int i = 0; i < selectedUpgrades.Count; i++)
            cards[i].SetUpgrade(selectedUpgrades[i]);

        upgradePanel.SetActive(true);
        inputBlocker.SetActive(true);
        inputBlockTimer = inputBlockTime;
        isBlockingInput = true;
    }

    public void UpgradeSelected(Upgrade upgrade)
    {
        if (isBlockingInput)
            return;
        upgrade.Apply(playerStats);

        SaveUpgradeLevels();

        upgradePanel.SetActive(false);
        if (!gameManager.IsEnding)
        {
            CursorManager.HideCursor();
        }
        if (!gameManager.IsEnding)
            Time.timeScale = 1f;

        playerXP.FinishUpgradeSelection();
        playerInput.EnableInput();
    }

    private List<Upgrade> GetRandomUpgrades(int count)
    {
        List<Upgrade> available = new List<Upgrade>();

        foreach (var upgrade in upgrades)
        {
            if (upgrade.CanUpgrade(playerStats))
                available.Add(upgrade);
        }

        List<Upgrade> result = new List<Upgrade>();

        while (count > 0 && available.Count > 0)
        {
            int index = Random.Range(0, available.Count);

            result.Add(available[index]);
            available.RemoveAt(index);

            count--;
        }

        return result;
    }

    private void LoadUpgradeLevels()
    {
        foreach (var save in RunData.Instance.upgrades)
        {
            foreach (var upgrade in upgrades)
            {
                if (upgrade.Id == save.id)
                {
                    upgrade.SetLevel(save.level);
                    break;
                }
            }
        }
    }

    private void ApplyLoadedUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            int savedLevel = upgrade.Level;

            upgrade.SetLevel(0);

            bool isScoreMultiplier =
                upgrade is ScoreMultiplierUpgrade;

            if (isScoreMultiplier)
            {
                upgrade.SetLevel(savedLevel);
                continue;
            }

            for (int i = 0; i < savedLevel; i++)
                upgrade.Apply(playerStats);

            upgrade.SetLevel(savedLevel);
        }
    }

    private void SaveUpgradeLevels()
    {
        RunData.Instance.upgrades.Clear();

        foreach (var upgrade in upgrades)
        {
            RunData.Instance.upgrades.Add(
                new UpgradeSaveData(
                    upgrade.Id,
                    upgrade.Level
                )
            );
        }
    }
}