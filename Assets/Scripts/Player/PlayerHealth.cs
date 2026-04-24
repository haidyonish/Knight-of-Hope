using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenTickRate = 1f;

    private PlayerStats stats;

    private float nextRegenTime = 0;

    protected override void Update()
    {
        base.Update();

        if (Time.time > nextRegenTime)
        {
            nextRegenTime = Time.time + regenTickRate;
            currentHealth = Mathf.Min(stats.MaxHealth, currentHealth + stats.Regen);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        soundManager.PlayPlayerHurt();
    }

    protected override void Awake()
    {
        stats = GetComponent<PlayerStats>();
        maxHealth = stats.MaxHealth;
        base.Awake();
    }

    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }
}
