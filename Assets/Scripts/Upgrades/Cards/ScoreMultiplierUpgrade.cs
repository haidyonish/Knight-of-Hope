using UnityEngine;

public class ScoreMultiplierUpgrade : Upgrade
{
    private float value = 0.05f;

    public ScoreMultiplierUpgrade(Sprite sprite)
    {
        CardSprite = sprite;

        Id = "upgrade_score_multiplier";

        Name = LocalizationManager.Instance.GetText("upgrade_score_multiplier_name");

        Description = LocalizationManager.Instance.GetText("upgrade_score_multiplier_desc");

        maxLevel = 10;
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddScoreMultiplier(value);

        level++;
    }
}