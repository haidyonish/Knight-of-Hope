using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private Transform fillAnchor;

    [Header("Visibility")]
    [SerializeField] private float visibleTime = 2f;

    private float hideTimer;

    private void Awake()
    {
        visuals.SetActive(false);
    }

    private void Update()
    {
        if (hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
                visuals.SetActive(false);
        }
    }

    public void Show(float currentHealth, float maxHealth)
    {
        visuals.SetActive(true);
        hideTimer = visibleTime;
        float percent = Mathf.Clamp01(currentHealth / maxHealth);
        fillAnchor.localScale = new Vector3(percent, 1f, 1f);
    }
}