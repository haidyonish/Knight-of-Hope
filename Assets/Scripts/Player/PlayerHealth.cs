using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float regenEfficiency = 0.5f;
    [SerializeField] private StatBars statBars;

    private PlayerStats stats;

    private PlayerStats Stats
    {
        get
        {
            if (stats == null)
                stats = GetComponent<PlayerStats>();
            return stats;
        }
    }

    protected override void Awake()
    {
        maxHealth = Stats.MaxHealth;
        base.Awake();
    }

    private void Start()
    {
        if (statBars != null)
            statBars.SetHPInstant(currentHealth / Stats.MaxHealth);
    }

    protected override void Update()
    {
        base.Update();
        if (Stats.Regen > 0f && currentHealth < Stats.MaxHealth)
        {
            currentHealth += Stats.Regen * Time.deltaTime * regenEfficiency;
            currentHealth = Mathf.Min(currentHealth, Stats.MaxHealth);
            if (statBars != null)
                statBars.SetHP(currentHealth / Stats.MaxHealth);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (statBars != null)
            statBars.SetHP(currentHealth / Stats.MaxHealth);
        soundManager.PlayPlayerHurt();
    }

    public void IncreaseMaxHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, Stats.MaxHealth);
        if (statBars != null)
            statBars.SetHP(currentHealth / Stats.MaxHealth);
    }

    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }
}