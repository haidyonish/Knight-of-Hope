using UnityEngine;

public class SpeedUpgrade : Upgrade
{
    private float value = .5f;

    public SpeedUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "upgrade_speed";

        Name = LocalizationManager.Instance.GetText("upgrade_speed_name");

        Description = LocalizationManager.Instance.GetText("upgrade_speed_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddMoveSpeed(value);
        level++;
    }
}