using UnityEngine;

public class ExperienceUpgrade : Upgrade
{
    private float value = 0.20f;

    public ExperienceUpgrade(Sprite sprite)
    {
        CardSprite = sprite;
        Id = "upgrade_experience";

        Name = LocalizationManager.Instance.GetText("upgrade_experience_name");

        Description = LocalizationManager.Instance.GetText("upgrade_experience_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddExperienceMultiplier(value);
        level++;
    }
}