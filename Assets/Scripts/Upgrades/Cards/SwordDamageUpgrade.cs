using UnityEngine;

public class SwordDamageUpgrade : Upgrade
{
    private float value = 1f;

    public SwordDamageUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "upgrade_sword_damage";

        Name = LocalizationManager.Instance.GetText("upgrade_sword_damage_name");

        Description = LocalizationManager.Instance.GetText("upgrade_sword_damage_desc");
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordDamage(value);
        level++;
    }
}