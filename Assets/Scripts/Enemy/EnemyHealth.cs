using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private PlayerXP playerXP;

    [SerializeField] private float xpAmount = 5f;

    public void SetPlayerXP(PlayerXP playerXP)
    {
        this.playerXP = playerXP;
    }

    protected override void Die()
    {
        base.Die();
        playerXP.AddXP(xpAmount);
    }
}
