using UnityEngine;

public class RangedEnemyCombat : MonoBehaviour
{
    [SerializeField] private RockProjectile rockPrefab;

    private SoundManager soundManager;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float throwForce = 7f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 throwDirection = new Vector2(1f, 0.7f);
    [SerializeField] private Transform rockSpawnPoint;

    private Animator animator;
    private RangedEnemyMovement movement;

    private float nextAttackTime;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<RangedEnemyMovement>();
    }

    private void Update()
    {
        if (!movement.IsInPosition)
            return;
        if (Time.time < nextAttackTime)
            return;
        nextAttackTime = Time.time + attackCooldown;
        animator.SetTrigger("Attack");
    }

    public void ThrowRock()
    {
        Vector2 direction = movement.IsFacingRight ? throwDirection : new Vector2(-throwDirection.x, throwDirection.y);
        Vector2 spawnPosition = rockSpawnPoint.position;
        soundManager?.PlayRockThrow();
        RockProjectile rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        rock.Setup(direction, throwForce, damage, soundManager);
    }

    public void ApplyDamageMultiplier(float multiplier)
    {
        damage *= multiplier;
    }

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }
}