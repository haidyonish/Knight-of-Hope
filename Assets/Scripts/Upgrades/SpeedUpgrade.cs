using UnityEngine;

public class SpeedUpgrade : Upgrade
{
    private float value = 1f;

    public SpeedUpgrade(Sprite cardSprite)
    {
        CardSprite = cardSprite;
        Id = "speed";
        Name = "Скорость";
        Description = "Увеличивает скорость перемещения на 1 единицу";
    }

    public override void Apply(PlayerStats stats)
    {
        stats.AddMoveSpeed(value);
        level++;
    }
}