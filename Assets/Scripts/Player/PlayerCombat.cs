using UnityEngine;

public class PlayerCombat : EntityCombat
{
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
    }

    protected override float GetAttackRange()
    {
        return stats != null ? stats.SwordRange : 1f;
    }
}