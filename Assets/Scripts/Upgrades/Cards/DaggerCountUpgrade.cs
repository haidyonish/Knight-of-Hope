using UnityEngine;

public class DaggerCountUpgrade : Upgrade
{
    public DaggerCountUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        Id = "upgrade_dagger_count";

        Name = LocalizationManager.Instance.GetText("upgrade_dagger_count_name");

        Description = LocalizationManager.Instance.GetText("upgrade_dagger_count_desc");

        maxLevel = 2;
    }

    public override bool CanUpgrade(PlayerStats stats)
    {
        return stats.DaggersUnlocked && base.CanUpgrade(stats);
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDaggerCount(1);

        level++;
    }
}