using UnityEngine;

public class DamageMultiplierUpgrade : Upgrade
{
    private float value = 0.10f;

    public DamageMultiplierUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "upgrade_damage_multiplier";

        Name = LocalizationManager.Instance.GetText("upgrade_damage_multiplier_name");

        Description = LocalizationManager.Instance.GetText("upgrade_damage_multiplier_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDamageMultiplier(value);
        level++;
    }
}