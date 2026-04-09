public abstract class Upgrade
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    protected int level = 0;
    protected int maxLevel = 5;

    public bool CanUpgrade()
    {
        return level < maxLevel;
    }

    public abstract void Apply(PlayerStats stats);
}