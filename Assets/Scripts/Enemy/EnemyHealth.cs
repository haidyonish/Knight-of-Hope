using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private SoundManager soundManager;
    private PlayerXP playerXP;

    [SerializeField] private float xpAmount = 5f;

    public void SetPlayerXP(PlayerXP playerXP)
    {
        this.playerXP = playerXP;
    }

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        soundManager.PlayEnemyHit();
    }

    protected override void Die()
    {
        base.Die();
        soundManager.PlayEnemyDeath();
        playerXP.AddXP(xpAmount);
    }
}
