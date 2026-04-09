public class SpeedUpgrade : Upgrade
{
    private float value = 1f;

    public SpeedUpgrade()
    {
        Name = "+1 Speed";
        Description = "Increase move speed";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddMoveSpeed(value);
        level++;
    }
}