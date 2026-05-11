using UnityEngine;

public abstract class Upgrade
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public Sprite CardSprite { get; protected set; }

    protected int level = 0;
    protected int maxLevel = 5;

    public int Level => level;
    public int MaxLevel => maxLevel;

    public virtual bool CanUpgrade(PlayerStats stats)
    {
        return level < maxLevel;
    }

    public abstract void Apply(PlayerStats stats);

    public void SetLevel(int value)
    {
        level = value;
    }
}