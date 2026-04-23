using UnityEngine;

public class SwordDamageUpgrade : Upgrade
{
    private float value = 1f;

    public SwordDamageUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;

        Name = "Урон меча";
        Description = "Увеличивает урон меча на 1 единицу";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordDamage(value);
        level++;
    }
}