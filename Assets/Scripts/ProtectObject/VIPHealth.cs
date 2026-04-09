using UnityEngine;

public class VIPHealth : EntityHealth
{
    [SerializeField] private GameManager gameManager;

    public override void TakeDamage(float damage)
    {
        currentHealth -= 1;
        PlayDamageFeedback();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }
}
