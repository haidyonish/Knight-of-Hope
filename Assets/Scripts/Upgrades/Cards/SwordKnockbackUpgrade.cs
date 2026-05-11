using UnityEngine;

public class SwordKnockbackUpgrade : Upgrade
{
    private float value = 0.7f;

    public SwordKnockbackUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        maxLevel = 3;

        Id = "upgrade_sword_knockback";

        Name = LocalizationManager.Instance.GetText("upgrade_sword_knockback_name");

        Description = LocalizationManager.Instance.GetText("upgrade_sword_knockback_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordKnockback(value);

        level++;
    }
}