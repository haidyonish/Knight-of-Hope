using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private PlayerHealth PlayerHealth
    {
        get
        {
            if (playerHealth == null)
                playerHealth = GetComponent<PlayerHealth>();

            return playerHealth;
        }
    }

    private void Start()
    {
        ApplyDifficulty();
    }

    [Header("Sword")]
    [SerializeField] private float swordDamage = 5f;
    [SerializeField] private float swordRange = 1f;
    [SerializeField] private float swordKnockback = 0f;

    public float SwordDamage => swordDamage;
    public float SwordRange => swordRange;
    public float SwordKnockback => swordKnockback;

    public float FinalSwordDamage => swordDamage * (1 + damageMultiplier);

    [Header("Daggers")]
    [SerializeField] private float daggerDamage = 3f;
    [SerializeField] private int daggerCount = 0;
    [SerializeField] private int daggerPenetration = 0;
    [SerializeField] private bool daggersUnlocked;
    [SerializeField] private float daggerCooldown = 5f;

    public float DaggerDamage => daggerDamage;
    public int DaggerCount => daggerCount;
    public int DaggerPenetration => daggerPenetration;
    public bool DaggersUnlocked => daggersUnlocked;
    public float DaggerCooldown => daggerCooldown;

    public float FinalDaggerDamage => daggerDamage * (1 + damageMultiplier);

    [Header("Player")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float regen = 0;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float MaxHealth => maxHealth;
    public float Regen => regen;

    [Header("Global Modifiers")]
    [SerializeField] private float damageMultiplier = 0f;
    [SerializeField] private float experienceMultiplier = 0f;
    [SerializeField] private float hopeMultiplier = 0f;

    public float DamageMultiplier => damageMultiplier;
    public float ExperienceMultiplier => experienceMultiplier;
    public float HopeMultiplier => hopeMultiplier;


    [Header("Game Balance")]
    [SerializeField] private float enemySpawnRate = 1f;

    public float EnemySpawnRate => enemySpawnRate;

    // ===== Methods for modifiers =====

    public void AddSwordDamage(float value) => swordDamage += value;
    public void AddSwordRange(float value) => swordRange += value;
    public void AddSwordKnockback(float value) => swordKnockback += value;
    public void AddDamageMultiplier(float value) => damageMultiplier += value;

    public void AddDaggerCount(int value) => daggerCount += value;
    public void AddDaggerDamage(float value) => daggerDamage += value;
    public void AddDaggerPenetration(int value) => daggerPenetration += value;

    public void UnlockDaggers() => daggersUnlocked = true;

    public void AddMoveSpeed(float value) => moveSpeed += value;

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;

        if (PlayerHealth != null)
            PlayerHealth.IncreaseMaxHealth(amount);
    }

    public void AddHealthRegen(float value) => regen += value;

    public void AddExperienceMultiplier(float value)
    {
        experienceMultiplier += value;
    }
    public void AddScoreMultiplier(float value)
    {
        RunStats.Instance.scoreMultiplier += value;
    }

    private void ApplyDifficulty()
    {
        AddSwordDamage(
            DifficultyManager.BonusSwordDamage
        );

        AddDaggerDamage(
            DifficultyManager.BonusDaggerDamage
        );

        AddMoveSpeed(
            DifficultyManager.BonusMoveSpeed
        );

        AddExperienceMultiplier(
            DifficultyManager.BonusExperienceMultiplier
        );
    }
}