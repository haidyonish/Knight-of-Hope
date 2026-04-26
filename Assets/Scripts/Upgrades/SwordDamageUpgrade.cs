using UnityEngine;

public class SwordDamageUpgrade : Upgrade
{
    private float value = 1f;

    public SwordDamageUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "sword_damage";
        Name = "Острота";
        Description = "Увеличивает урон меча на 1 единицу";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordDamage(value);
        level++;
    }
}