public class SwordDamageUpgrade : Upgrade
{
    private float value = 1f;

    public SwordDamageUpgrade()
    {
        Name = "+1 Sword damage";
        Description = "Increase sword damage";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordDamage(value);
        level++;
    }
}