using UnityEngine;

public class EnemyCombat : EntityCombat
{
    [SerializeField] float damage = 5;
    [SerializeField] float range = 1f;

    private void FixedUpdate()
    {
        CheckTargets();
    }

    public override void DamageTargets()
    {
        Collider2D[] targets = GetTargets(range);

        foreach (var t in targets)
        {
            EntityHealth health = t.GetComponent<EntityHealth>();

            if (health != null)
                health.TakeDamage(damage);
        }
    }

    private void CheckTargets()
    {
        if (!movement.IsGrounded)
            return;

        if (GetTargets(range).Length != 0)
            animator.SetTrigger("Attack");
    }

    protected override float GetAttackRange()
    {
        return range;
    }
}