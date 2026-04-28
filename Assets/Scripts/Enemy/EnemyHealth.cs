using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private SoundManager soundManager;
    private PlayerXP playerXP;
    private RunStatsManager runStatsManager;
    private float enterTime;
    private bool hasEntered = false;

    [SerializeField] private float xpAmount = 5f;

    public void SetPlayerXP(PlayerXP playerXP)
    {
        this.playerXP = playerXP;
    }

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public void SetRunStatsManager(RunStatsManager runStatsManager)
    {
        this.runStatsManager = runStatsManager;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        soundManager.PlayEnemyHit();
    }

    public void OnEnterLevel()
    {
        enterTime = Time.time;
    }

    protected override void Die()
    {
        base.Die();

        float lifeTime = enterTime > 0f ? Time.time - enterTime : 0f;
        lifeTime = Mathf.Max(0f, lifeTime);
        runStatsManager.AddEnemyKillTimePenalty(lifeTime);

        soundManager.PlayEnemyDeath();
        playerXP.AddXP(xpAmount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasEntered)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyEnter"))
        {
            hasEntered = true;
            enterTime = Time.time;
        }
    }
}
