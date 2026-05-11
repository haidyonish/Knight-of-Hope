using UnityEngine;

public class DaggerUnlockUpgrade : Upgrade
{
    public DaggerUnlockUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        Id = "upgrade_dagger_unlock";

        Name = LocalizationManager.Instance.GetText("upgrade_dagger_unlock_name");

        Description = LocalizationManager.Instance.GetText("upgrade_dagger_unlock_desc");

        maxLevel = 1;
    }

    public override bool CanUpgrade(PlayerStats stats)
    {
        return !stats.DaggersUnlocked && base.CanUpgrade(stats);
    }

    public override void Apply(PlayerStats stats)
    {
        stats.UnlockDaggers();
        stats.AddDaggerCount(1);

        level++;
    }
}