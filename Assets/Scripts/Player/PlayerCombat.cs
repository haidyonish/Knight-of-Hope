using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Audio")]
    [SerializeField] private SoundManager soundManager;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.2f;

    [Header("Daggers")]
    [SerializeField] private DaggerProjectile daggerPrefab;
    [SerializeField] private Vector2 daggerOffset = new Vector2(0.6f, 0.1f);
    [SerializeField] private float daggerDelay = 0.15f;

    private PlayerStats stats;

    private float nextAttackTime;
    private float nextDaggerTime;
    private float nextSingleDaggerTime;

    private int daggersToSpawn;

    protected override void Awake()
    {
        base.Awake();
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;
        HandleDaggers();
        HandleQueuedDaggers();
    }

    public void RequestAttack()
    {
        if (Time.time < nextAttackTime)
            return;
        if (!movement.IsGrounded)
            return;
        nextAttackTime = Time.time + attackCooldown;
        animator.SetTrigger("Attack");
    }

    public override void DamageTargets()
    {
        Collider2D[] targets = GetTargets(stats.SwordRange);
        if (targets.Length > 0)
        {
            soundManager.PlaySwordHitEnemy();
            foreach (var t in targets)
            {
                EntityHealth health = t.GetComponent<EntityHealth>();
                if (health != null)
                    health.TakeDamage(stats.FinalSwordDamage, stats.SwordKnockback, transform.position);
            }
        }
        else
        {
            soundManager.PlaySwordSwing();
        }
    }

    protected override float GetAttackRange()
    {
        return stats != null ? stats.SwordRange : 1f;
    }

    private void HandleDaggers()
    {
        if (!stats.DaggersUnlocked)
            return;
        if (Time.time < nextDaggerTime)
            return;
        nextDaggerTime = Time.time + stats.DaggerCooldown;
        daggersToSpawn = stats.DaggerCount;
        SpawnDagger();
        daggersToSpawn--;
        nextSingleDaggerTime = Time.time + daggerDelay;
    }

    private void HandleQueuedDaggers()
    {
        if (daggersToSpawn <= 0)
            return;
        if (Time.time < nextSingleDaggerTime)
            return;
        SpawnDagger();
        daggersToSpawn--;
        nextSingleDaggerTime = Time.time + daggerDelay;
    }

    private void SpawnDagger()
    {
        Vector2 direction = movement.IsFacingRight ? Vector2.right : Vector2.left;
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(daggerOffset.x * direction.x, daggerOffset.y);
        DaggerProjectile dagger = Instantiate(daggerPrefab, spawnPosition, Quaternion.identity);
        dagger.Setup(direction, stats.FinalDaggerDamage, stats.DaggerPenetration, soundManager);
    }
}