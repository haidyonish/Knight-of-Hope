using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform credits;
    [SerializeField] private RectTransform settings;
    [SerializeField] private RectTransform leaderboard;
    [SerializeField] private GameObject nameInputPanel;

    [Header("Leaderboard")]
    [SerializeField] private LeaderboardUI leaderboardUI;

    [Header("Animation")]
    [SerializeField] private float animationSpeed = 8f;

    [Header("Intro")]
    [SerializeField] private IntroSlides introSlides;

    private readonly Vector2 hiddenPos = new Vector2(350f, -1000f);
    private readonly Vector2 shownPos = new Vector2(350f, 0f);

    private Vector2 creditsTarget;
    private Vector2 settingsTarget;
    private Vector2 leaderboardTarget;

    private void Awake()
    {
        credits.anchoredPosition = hiddenPos;
        settings.anchoredPosition = hiddenPos;
        leaderboard.anchoredPosition = hiddenPos;
        creditsTarget = hiddenPos;
        settingsTarget = hiddenPos;
        leaderboardTarget = hiddenPos;
        PlayerProfile.Load();
        if (!PlayerProfile.HasName)
            nameInputPanel.SetActive(true);
    }

    private void Update()
    {
        AnimatePanel(credits, creditsTarget);
        AnimatePanel(settings, settingsTarget);
        AnimatePanel(leaderboard, leaderboardTarget);
    }

    public void StartGame()
    {
        RunData.Instance.ResetRun();
        RunStats.Instance.ResetStats();
        introSlides.Play();
    }

    public void ToggleCredits()
    {
        bool isOpen = Vector2.Distance(creditsTarget, shownPos) < 1f;
        if (isOpen)
        {
            creditsTarget = hiddenPos;
        }
        else
        {
            creditsTarget = shownPos;
            settingsTarget = hiddenPos;
            leaderboardTarget = hiddenPos;
        }
    }

    public void ToggleSettings()
    {
        bool isOpen = Vector2.Distance(settingsTarget, shownPos) < 1f;
        if (isOpen)
        {
            settingsTarget = hiddenPos;
        }
        else
        {
            settingsTarget = shownPos;
            creditsTarget = hiddenPos;
            leaderboardTarget = hiddenPos;
        }
    }

    public void ToggleLeaderboard()
    {
        bool isOpen = Vector2.Distance(leaderboardTarget, shownPos) < 1f;
        if (isOpen)
        {
            leaderboardTarget = hiddenPos;
        }
        else
        {
            leaderboardTarget = shownPos;
            creditsTarget = hiddenPos;
            settingsTarget = hiddenPos;
            leaderboardUI.Refresh();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void AnimatePanel(RectTransform panel, Vector2 target)
    {
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, animationSpeed * Time.unscaledDeltaTime);
    }
}