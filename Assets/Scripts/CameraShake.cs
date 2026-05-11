using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private float defaultDuration = 0.15f;
    [SerializeField] private float defaultStrength = 0.15f;

    private Vector3 originalPosition;

    private Coroutine shakeRoutine;

    private void Awake()
    {
        Instance = this;

        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        Shake(
            defaultDuration,
            defaultStrength
        );
    }

    public void Shake(
        float duration,
        float strength
    )
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine =
            StartCoroutine(
                ShakeRoutine(
                    duration,
                    strength
                )
            );
    }

    private IEnumerator ShakeRoutine(
        float duration,
        float strength
    )
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            Vector2 offset =
                Random.insideUnitCircle * strength;

            transform.localPosition =
                originalPosition +
                new Vector3(
                    offset.x,
                    offset.y,
                    0f
                );

            yield return null;
        }

        transform.localPosition =
            originalPosition;
    }
}