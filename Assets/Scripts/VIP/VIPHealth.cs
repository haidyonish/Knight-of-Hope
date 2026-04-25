using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum VIPType
{
    King,
    Queen,
    Princess,
    Prince
}

public class VIPHealth : EntityHealth
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HeartsUI heartsUI;


    [SerializeField] private VIPType vipType;

    public override void TakeDamage(float damage)
    {
        currentHealth -= 1;
        heartsUI.RefreshHearts();
        PlayHitSound();
        PlayDamageFeedback();

        if (currentHealth <= 0)
            Die();
    }

    private void PlayHitSound()
    {
        switch (vipType)
        {
            case VIPType.King:
                soundManager.PlayKingHurt();
                break;

            case VIPType.Queen:
                soundManager.PlayQueenHurt();
                break;

            case VIPType.Princess:
                soundManager.PlayPrincessHurt();
                break;

            case VIPType.Prince:
                soundManager.PlayPrinceHurt();
                break;
        }
    }

    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }
}