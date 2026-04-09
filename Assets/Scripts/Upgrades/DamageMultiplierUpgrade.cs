public class DamageMultiplierUpgrade : Upgrade
{
    private float value = .1f;

    public DamageMultiplierUpgrade()
    {
        Name = "+10% Damage";
        Description = "Increase general damage";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddDamageMultiplier(value);
        level++;
    }
}