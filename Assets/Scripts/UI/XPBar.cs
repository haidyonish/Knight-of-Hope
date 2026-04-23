using UnityEngine;

public class XPBar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;

    [SerializeField] private float minWidth = 43.7f;
    [SerializeField] private float maxWidth = 762.6f;

    [SerializeField] private float smoothTime = 0.2f;

    private float targetProgress = 0f;
    private float currentProgress = 0f;
    private float velocity = 0f;

    public void SetProgress(float progress)
    {
        targetProgress = Mathf.Clamp01(progress);
    }

    private void Update()
    {
        currentProgress = Mathf.SmoothDamp(
            currentProgress,
            targetProgress,
            ref velocity,
            smoothTime
        );

        float width = Mathf.Lerp(
            minWidth,
            maxWidth,
            currentProgress
        );

        fill.sizeDelta = new Vector2(
            width,
            fill.sizeDelta.y
        );
    }
}