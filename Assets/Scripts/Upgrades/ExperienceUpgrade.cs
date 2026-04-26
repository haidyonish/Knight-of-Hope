using UnityEngine;

public class ExperienceUpgrade : Upgrade
{
    private float value = 0.20f;

    public ExperienceUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "experience";
        Name = "Мудрость";
        Description = "Вы получаете на 20% больше опыта";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddExperienceMultiplier(value);
        level++;
    }
}