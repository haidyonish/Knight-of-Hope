// UpgradeManager.cs
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CardUI[] cards;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private PlayerXP playerXP;

    [Header("Card Sprites")]
    [SerializeField] private Sprite damageMultiplierCard;
    [SerializeField] private Sprite swordDamageCard;
    [SerializeField] private Sprite speedCard;
    [SerializeField] private Sprite swordRangeCard;

    private List<Upgrade> upgrades = new List<Upgrade>();

    public bool IsChoosingUpgrade => upgradePanel.activeSelf;

    private void Awake()
    {
        upgrades.Add(new DamageMultiplierUpgrade(damageMultiplierCard));
        upgrades.Add(new SwordDamageUpgrade(swordDamageCard));
        upgrades.Add(new SpeedUpgrade(speedCard));
        upgrades.Add(new SwordRangeUpgrade(swordRangeCard));

        LoadUpgradeLevels();
        ApplyLoadedUpgrades();
    }

    public void ShowUpgrades()
    {
        Time.timeScale = 0f;

        List<Upgrade> selectedUpgrades = GetRandomUpgrades(3);

        for (int i = 0; i < selectedUpgrades.Count; i++)
            cards[i].SetUpgrade(selectedUpgrades[i]);

        upgradePanel.SetActive(true);
    }

    public void UpgradeSelected(Upgrade upgrade)
    {
        upgrade.Apply(playerStats);

        SaveUpgradeLevels();

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;

        playerXP.FinishUpgradeSelection();
    }

    private List<Upgrade> GetRandomUpgrades(int count)
    {
        List<Upgrade> available = new List<Upgrade>();

        foreach (var upgrade in upgrades)
        {
            if (upgrade.CanUpgrade())
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