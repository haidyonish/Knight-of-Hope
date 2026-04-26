using UnityEngine;

public class DamageMultiplierUpgrade : Upgrade
{
    private float value = 0.15f;

    public DamageMultiplierUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "damage_multiplier";
        Name = "Ярость";
        Description = "Увеличивает общий урон на 15%";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDamageMultiplier(value);
        level++;
    }
}