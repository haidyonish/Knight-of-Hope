using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CardUI[] cards;
    [SerializeField] private GameObject upgradePanel;
    private List<Upgrade> upgrades = new List<Upgrade>();

    private void Awake()
    {
        upgrades.Add(new SwordDamageUpgrade());
        upgrades.Add(new SpeedUpgrade());
        upgrades.Add(new SwordRangeUpgrade());
        upgrades.Add(new DamageMultiplierUpgrade());
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
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }

    private List<Upgrade> GetRandomUpgrades(int count)
    {
        List<Upgrade> copyUpgrades = new List<Upgrade>();
        List<Upgrade> outUpgrades = new List<Upgrade>();
        for (int i = 0; i < upgrades.Count; i++)
        {
            copyUpgrades.Add(upgrades[i]);
        }
        while (count > 0)
        {
            int index = Random.Range(0, copyUpgrades.Count);
            if (copyUpgrades[index].CanUpgrade())
            {
                outUpgrades.Add(copyUpgrades[index]);
                copyUpgrades.Remove(copyUpgrades[index]);
                count--;
            }
        }

        return outUpgrades;
    }
}
