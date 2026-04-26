using UnityEngine;

public class MaxHealthUpgrade : Upgrade
{
    private float value = 20f;

    public MaxHealthUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "max_health";
        Name = "Живучесть";
        Description = "Увеличивает максимум HP на 20";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddMaxHealth(value);
        level++;
    }
}