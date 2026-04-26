using UnityEngine;

public class SwordRangeUpgrade : Upgrade
{
    private float value = 0.2f;

    public SwordRangeUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        maxLevel = 3;
        Id = "sword_range";
        Name = "Размах";
        Description = "Увеличивает дальность атаки меча на 0.2 единицы";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordRange(value);
        level++;
    }
}