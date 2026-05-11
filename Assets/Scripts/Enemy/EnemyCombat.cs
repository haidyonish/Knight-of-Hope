using UnityEngine;

public class EnemyCombat : EntityCombat
{
    private SoundManager soundManager;

    [Header("Damage")]
    [SerializeField] private float damage = 5f;

    [Header("Attack Point")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackSize = new Vector2(1f, 1f);

    private bool _isAttacking;

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public void ApplyDamageMultiplier(float multiplier)
    {
        damage *= multiplier;
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
        if (GetTargets().Length > 0)
        {
            _isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    private Collider2D[] GetTargets()
    {
        return Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0f, targetLayer);
    }

    public override void DamageTargets()
    {
        Collider2D[] targets = GetTargets();
        if (targets.Length > 0)
            soundManager?.PlayEnemyHit();
        foreach (var target in targets)
        {
            EntityHealth health = target.GetComponent<EntityHealth>();
            if (health != null)
                health.TakeDamage(damage);
        }
    }

    public void OnAttackFinished()
    {
        _isAttacking = false;
    }

    protected override void OnDrawGizmos()
    {
        showAttackGizmo = false;
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }
}