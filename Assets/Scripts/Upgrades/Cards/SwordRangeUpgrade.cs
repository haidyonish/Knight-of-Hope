using UnityEngine;

public class SwordRangeUpgrade : Upgrade
{
    private float value = 0.1f;

    public SwordRangeUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        maxLevel = 3;
        Id = "upgrade_sword_range";

        Name = LocalizationManager.Instance.GetText("upgrade_sword_range_name");

        Description = LocalizationManager.Instance.GetText("upgrade_sword_range_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordRange(value);
        level++;
    }
}