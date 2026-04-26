using UnityEngine;

public class HealthRegenUpgrade : Upgrade
{
    private float value = 1f;

    public HealthRegenUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "health_regen";
        Name = "Регенерация";
        Description = "Восстанавливает HP со временем";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddHealthRegen(value);
        level++;
    }
}