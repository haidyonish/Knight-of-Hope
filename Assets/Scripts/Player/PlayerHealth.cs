using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenEfficiency = 0.5f;
    [SerializeField] private StatBars statBars;

    private PlayerStats stats;

    protected override void Awake()
    {
        stats = GetComponent<PlayerStats>();

        maxHealth = stats.MaxHealth;
        base.Awake();
    }

    private void Start()
    {
        statBars.SetHPInstant(currentHealth / stats.MaxHealth);
    }

    protected override void Update()
    {
        base.Update();

        if (stats.Regen > 0f && currentHealth < stats.MaxHealth)
        {
            currentHealth +=
                stats.Regen *
                Time.deltaTime *
                regenEfficiency;

            currentHealth =
                Mathf.Min(currentHealth, stats.MaxHealth);

            statBars.SetHP(currentHealth / stats.MaxHealth);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        statBars.SetHP(currentHealth / stats.MaxHealth);
        soundManager.PlayPlayerHurt();
    }

    public void IncreaseMaxHealth(float amount)
    {
        currentHealth += amount;

        currentHealth =
            Mathf.Min(currentHealth, stats.MaxHealth);

        statBars.SetHP(currentHealth / stats.MaxHealth);
    }

    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }
}