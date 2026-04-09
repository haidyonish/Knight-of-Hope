using System.Collections;
using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigitbody;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = .1f;
    private bool isDamageFeedbackActive = false;
    private Material originalMaterial;
    private float damageFeedbackEnd;


    protected float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        EndDamageFeedback();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        PlayDamageFeedback();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(float damage, float knockback, Vector2 sourcePosition)
    {
        currentHealth -= damage;
        PlayDamageFeedback();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void PlayDamageFeedback()
    {
        spriteRenderer.material = damageMaterial;
        damageFeedbackEnd = Time.time + damageFeedbackDuration;
        isDamageFeedbackActive = true;
    }

    private void EndDamageFeedback()
    {
        if (isDamageFeedbackActive && Time.time > damageFeedbackEnd)
        {
            spriteRenderer.material = originalMaterial;
            isDamageFeedbackActive = false;
        }
    }

    protected virtual void Die()
    {
        animator.enabled = false;
        col.enabled = false;

        rigitbody.gravityScale = 12;
        rigitbody.linearVelocityY = 15;
    }
}
