using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private UpgradeManager upgradeManager;

    private float currentXP = 0;
    private int level = 0;

    private float XpToNextLevel => 10 + level * 5;

    public void AddXP(float amount)
    {
        currentXP += amount;
        if (currentXP >= XpToNextLevel) 
            LevelUp();
        ui.UpdateXPBar(currentXP, XpToNextLevel);
    }

    private void LevelUp()
    {
        currentXP -= XpToNextLevel;
        upgradeManager.ShowUpgrades();
        level += 1;
    }
}
