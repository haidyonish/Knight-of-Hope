using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float waveAmplitude = 0.15f;
    [SerializeField] private float waveFrequency = 8f;

    private SoundManager soundManager;

    private float damage;
    private int remainingPierce;

    private Vector2 direction;

    private float lifeTimer;
    private Vector2 basePosition;

    public void Setup(Vector2 direction, float damage, int pierce, SoundManager soundManager)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.soundManager = soundManager;

        remainingPierce = pierce;

        basePosition = transform.position;

        transform.localScale = new Vector3(direction.x > 0 ? 1f : -1f, 1f, 1f);
    }

    private void Start()
    {
        soundManager?.PlayDaggerSwing();

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        basePosition += direction * speed * Time.deltaTime;

        float wave = Mathf.Sin(lifeTimer * waveFrequency) * waveAmplitude;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        transform.position = basePosition + perpendicular * wave;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();

        if (health == null)
            return;

        health.TakeDamage(damage);

        soundManager?.PlayDaggerHit();

        remainingPierce--;

        damage *= 0.5f;

        if (remainingPierce < 0)
            Destroy(gameObject);
    }
}