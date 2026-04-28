using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform pausePanel;
    [SerializeField] private RectTransform settingsPanel;

    [Header("Animation")]
    [SerializeField] private float animationSpeed = 6f;

    private readonly Vector2 pauseShownPos = new Vector2(0f, -80f);
    private readonly Vector2 pauseHiddenPos = new Vector2(-570f, -80f);

    private readonly Vector2 settingsShownPos = new Vector2(350f, 0f);
    private readonly Vector2 settingsHiddenPos = new Vector2(350f, -1000f);

    private bool isAnimatingPause = false;
    private bool isAnimatingSettings = false;

    private bool pauseVisible = true;
    private bool settingsVisible = false;

    private void Awake()
    {
        pausePanel.anchoredPosition = pauseShownPos;
        settingsPanel.anchoredPosition = settingsHiddenPos;
    }

    private void Update()
    {
        Animate(pausePanel, pauseVisible ? pauseShownPos : pauseHiddenPos, ref isAnimatingPause);
        Animate(settingsPanel, settingsVisible ? settingsShownPos : settingsHiddenPos, ref isAnimatingSettings);
    }

    public void ToggleMenu()
    {
        pauseVisible = !pauseVisible;
        settingsVisible = !settingsVisible;

        isAnimatingPause = true;
        isAnimatingSettings = true;
    }

    public void ResetToPause()
    {
        pauseVisible = true;
        settingsVisible = false;

        isAnimatingPause = false;
        isAnimatingSettings = false;

        pausePanel.anchoredPosition = pauseShownPos;
        settingsPanel.anchoredPosition = settingsHiddenPos;
    }

    private void Animate(RectTransform panel, Vector2 target, ref bool animating)
    {
        if (!animating)
            return;

        panel.anchoredPosition = Vector2.Lerp(
            panel.anchoredPosition,
            target,
            animationSpeed * Time.unscaledDeltaTime
        );

        if (Vector2.Distance(panel.anchoredPosition, target) < 0.5f)
        {
            panel.anchoredPosition = target;
            animating = false;
        }
    }
}