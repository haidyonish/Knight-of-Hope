using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Sword")]
    [SerializeField] private float swordDamage = 5f;
    [SerializeField] private float swordRange = 1f;
    [SerializeField] private float swordKnockback = 1f;

    public float SwordDamage => swordDamage;
    public float SwordRange => swordRange;
    public float SwordKnockback => swordKnockback;

    public float FinalSwordDamage => swordDamage * (1 + damageMultiplier);


    [Header("Daggers")]
    [SerializeField] private float daggerDamage = 10f;
    [SerializeField] private int daggerCount = 1;
    [SerializeField] private int daggerPenetration = 1;

    public float DaggerDamage => daggerDamage;
    public int DaggerCount => daggerCount;
    public int DaggerPenetration => daggerPenetration;

    public float FinalDaggerDamage => daggerDamage * (1 + damageMultiplier);


    [Header("Aura")]
    [SerializeField] private float auraDamage = 1f;
    [SerializeField] private float auraRange = 1f;
    [SerializeField] private float auraTickRate = 1f;

    public float AuraDamage => auraDamage;
    public float AuraRange => auraRange;
    public float AuraTickRate => auraTickRate;

    public float FinalAuraDamage => auraDamage * (1 + damageMultiplier);


    [Header("Player")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxHealth = 100;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public int MaxHealth => maxHealth;


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
    public void AddDamageMultiplier(float value) => damageMultiplier += value;

    public void AddDaggerCount(int value) => daggerCount += value;
    public void AddDaggerDamage(float value) => daggerDamage += value;

    public void AddAuraDamage(float value) => auraDamage += value;

    public void AddMoveSpeed(float value) => moveSpeed += value;

    public void AddMaxHealth(int value) => maxHealth += value;
}