using UnityEngine;

public class HealthRegenUpgrade : Upgrade
{
    private float value = 0.5f;

    public HealthRegenUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "upgrade_health_regen";

        Name = LocalizationManager.Instance.GetText("upgrade_health_regen_name");

        Description = LocalizationManager.Instance.GetText("upgrade_health_regen_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddHealthRegen(value);
        level++;
    }
}