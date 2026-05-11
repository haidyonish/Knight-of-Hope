using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float gravity = 14f;
    [SerializeField] private float lifeTime = 8f;

    private Vector2 velocity;
    private float damage;
    private SoundManager soundManager;

    public void Setup(Vector2 direction, float throwForce, float damage, SoundManager soundManager)
    {
        velocity = direction.normalized * throwForce;
        this.damage = damage;
        this.soundManager = soundManager;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() == null && other.GetComponent<VIPHealth>() == null)
            return;
        EntityHealth health = other.GetComponent<EntityHealth>();
        if (health == null)
            return;
        health.TakeDamage(damage);
        soundManager?.PlayRockHit();
        Destroy(gameObject);
    }
}