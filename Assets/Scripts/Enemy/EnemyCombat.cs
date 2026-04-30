using UnityEngine;

public class EnemyCombat : EntityCombat
{
    private SoundManager soundManager;

    [SerializeField] private float damage = 5;
    [SerializeField] private float range = 1f;

    private bool _isAttacking;

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    private void FixedUpdate()
    {
        CheckTargets();
    }

    private void CheckTargets()
    {
        if (!movement.IsGrounded)
            return;

        if (_isAttacking)
            return;

        if (GetTargets(range).Length > 0)
        {
            _isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public override void DamageTargets()
    {
        Collider2D[] targets = GetTargets(range);

        if (targets.Length > 0)
        {
            soundManager?.PlayEnemyHit();
        }

        foreach (var t in targets)
        {
            EntityHealth health = t.GetComponent<EntityHealth>();

            if (health != null)
                health.TakeDamage(damage);
        }
    }

    public void OnAttackFinished()
    {
        _isAttacking = false;
    }

    protected override float GetAttackRange()
    {
        return range;
    }
}