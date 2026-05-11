using UnityEngine;

public class DaggerPenetrationUpgrade : Upgrade
{
    public DaggerPenetrationUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        Id = "upgrade_dagger_penetration";

        Name = LocalizationManager.Instance.GetText("upgrade_dagger_penetration_name");

        Description = LocalizationManager.Instance.GetText("upgrade_dagger_penetration_desc");

        maxLevel = 1;
    }

    public override bool CanUpgrade(PlayerStats stats)
    {
        return stats.DaggersUnlocked && base.CanUpgrade(stats);
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDaggerPenetration(1);

        level++;
    }
}