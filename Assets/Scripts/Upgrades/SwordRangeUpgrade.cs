public class SwordRangeUpgrade : Upgrade
{
    private float value = 0.2f;

    public SwordRangeUpgrade()
    {
        Name = "+0.2 Sword range";
        Description = "Increase sword attack range";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddSwordRange(value);
        level++;
    }
}