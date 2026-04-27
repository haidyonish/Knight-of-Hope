using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform credits;
    [SerializeField] private RectTransform settings;

    [Header("Animation")]
    [SerializeField] private float animationSpeed = 8f;

    private readonly Vector2 hiddenPos = new Vector2(350f, -1000f);
    private readonly Vector2 shownPos = new Vector2(350f, 20f);

    private Vector2 creditsTarget;
    private Vector2 settingsTarget;

    private void Awake()
    {
        credits.anchoredPosition = hiddenPos;
        settings.anchoredPosition = hiddenPos;

        creditsTarget = hiddenPos;
        settingsTarget = hiddenPos;
    }

    private void Update()
    {
        AnimatePanel(credits, creditsTarget);
        AnimatePanel(settings, settingsTarget);
    }

    public void StartGame()
    {
        RunData.Instance.ResetRun();
        SceneManager.LoadScene("Level1");
    }

    public void ToggleCredits()
    {
        bool isOpen =
            Vector2.Distance(
                creditsTarget,
                shownPos
            ) < 1f;

        if (isOpen)
        {
            creditsTarget = hiddenPos;
        }
        else
        {
            creditsTarget = shownPos;
            settingsTarget = hiddenPos;
        }
    }

    public void ToggleSettings()
    {
        bool isOpen =
            Vector2.Distance(
                settingsTarget,
                shownPos
            ) < 1f;

        if (isOpen)
        {
            settingsTarget = hiddenPos;
        }
        else
        {
            settingsTarget = shownPos;
            creditsTarget = hiddenPos;
        }
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void AnimatePanel(
        RectTransform panel,
        Vector2 target)
    {
        panel.anchoredPosition =
            Vector2.Lerp(
                panel.anchoredPosition,
                target,
                animationSpeed *
                Time.unscaledDeltaTime
            );
    }
}