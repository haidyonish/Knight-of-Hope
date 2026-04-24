using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Audio")]
    [SerializeField] private SoundManager soundManager;

    private PlayerStats stats;

    protected override void Awake()
    {
        base.Awake();
        stats = GetComponent<PlayerStats>();
    }

    public void RequestAttack()
    {
        if (!movement.IsGrounded)
            return;

        animator.SetTrigger("Attack");
    }

    public override void DamageTargets()
    {
        Collider2D[] targets = GetTargets(stats.SwordRange);

        if (targets.Length != 0)
        {
            soundManager.PlaySwordHitEnemy();
            foreach (var t in targets)
            {
                EntityHealth health = t.GetComponent<EntityHealth>();

                if (health != null)
                {
                    health.TakeDamage(
                        stats.FinalSwordDamage,
                        stats.SwordKnockback,
                        transform.position
                    );
                }
            }
        } else
            soundManager.PlaySwordSwing();
    }

    protected override float GetAttackRange()
    {
        return stats != null ? stats.SwordRange : 1f;
    }
}