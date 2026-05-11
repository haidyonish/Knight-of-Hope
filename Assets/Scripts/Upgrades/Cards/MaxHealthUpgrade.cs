using UnityEngine;

public class MaxHealthUpgrade : Upgrade
{
    private float value = 20f;

    public MaxHealthUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "upgrade_max_health";

        Name = LocalizationManager.Instance.GetText("upgrade_max_health_name");

        Description = LocalizationManager.Instance.GetText("upgrade_max_health_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddMaxHealth(value);
        level++;
    }
}