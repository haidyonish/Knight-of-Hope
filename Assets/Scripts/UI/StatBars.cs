using UnityEngine;

public class StatBars : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField] private RectTransform xpFill;
    [SerializeField] private RectTransform hpFill;

    [Header("Fill Settings")]
    [SerializeField] private float minWidth = 0f;
    [SerializeField] private float maxWidth = 725f;

    [Header("Animation")]
    [SerializeField] private float smoothTime = 0.2f;

    private float targetXP = 0f;
    private float currentXP = 0f;
    private float velocityXP = 0f;

    private float targetHP = 0f;
    private float currentHP = 0f;
    private float velocityHP = 0f;

    private void Update()
    {
        UpdateBar(ref currentXP, ref targetXP, ref velocityXP, xpFill);
        UpdateBar(ref currentHP, ref targetHP, ref velocityHP, hpFill);
    }

    public void SetXP(float progress)
    {
        targetXP = Mathf.Clamp01(progress);
    }

    public void SetHP(float progress)
    {
        targetHP = Mathf.Clamp01(progress);
    }

    public void SetHPInstant(float progress)
    {
        progress = Mathf.Clamp01(progress);
        currentHP = progress;
        targetHP = progress;
        velocityHP = 0f;
        ApplyWidth(hpFill, currentHP);
    }

    public void SetXPInstant(float progress)
    {
        progress = Mathf.Clamp01(progress);
        currentXP = progress;
        targetXP = progress;
        velocityXP = 0f;
        ApplyWidth(xpFill, currentXP);
    }

    private void UpdateBar(ref float current, ref float target, ref float velocity, RectTransform fill)
    {
        current = Mathf.SmoothDamp(current, target, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
        ApplyWidth(fill, current);
    }

    private void ApplyWidth(RectTransform fill, float progress)
    {
        float width = Mathf.Lerp(minWidth, maxWidth, progress);
        fill.sizeDelta = new Vector2(width, fill.sizeDelta.y);
    }
}