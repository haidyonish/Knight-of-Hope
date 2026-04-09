using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private GameManager gameManager;

    private PlayerStats stats;

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
