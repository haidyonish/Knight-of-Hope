using UnityEngine;

public class DamageMultiplierUpgrade : Upgrade
{
    private float value = 0.1f;

    public DamageMultiplierUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        Name = "Общий урон";
        Description = "Увеличивает общий урон на 10%";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDamageMultiplier(value);
        level++;
    }
}