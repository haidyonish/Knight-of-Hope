using UnityEngine;

public class EnemyHealth : EntityHealth
{
    [SerializeField] private EnemyHealthBar healthBar;

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

    public void ApplyHealthMultiplier(float multiplier)
    {
        maxHealth *= multiplier;
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damage, float knockback, Vector2 sourcePosition)
    {
        base.TakeDamage(damage, knockback, sourcePosition);
        if (currentHealth > 0)
            soundManager.PlayEnemyHit();
        healthBar.Show(CurrentHealth, MaxHealth);
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
        float maxTime = 10f;
        float bonus = Mathf.Max(0f, maxTime - lifeTime);
        runStatsManager.AddEnemyKillSpeedBonus(bonus);
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