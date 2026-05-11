using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private RunStatsManager runStatsManager;

    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private float inputBlockTime = 0.5f;

    private float inputBlockTimer = 0f;
    private bool isBlockingInput = false;

    [SerializeField] private float levelDuration = 30f;

    public float CurrentTime { get; private set; }
    public bool IsEnding => endingSlowMotion || victory || gameOver;

    private bool gameOver = false;
    private bool victory = false;
    private bool isTransitioning = false;
    private bool isPaused = false;
    private bool endingSlowMotion = false;
    private bool endingWin = false;
    private float slowMotionDuration = 1f;
    private float slowMotionTimer = 0f;

    public float LevelDuration => levelDuration;

    private void Awake()
    {
        Time.timeScale = 1f;
        CursorManager.HideCursor();
        isTransitioning = false;
        isPaused = false;
    }

    private void Update()
    {
        if (isBlockingInput)
        {
            inputBlockTimer -= Time.unscaledDeltaTime;

            if (inputBlockTimer <= 0f)
            {
                isBlockingInput = false;
                inputBlocker.SetActive(false);
            }
        }

        if (endingSlowMotion)
        {
            UpdateEndingSlowMotion();
            return;
        }

        if (isPaused || gameOver || victory)
            return;

        CurrentTime += Time.deltaTime;

        if (CurrentTime > levelDuration)
            Victory();
    }

    public void TogglePause()
    {
        if (
            gameOver ||
            victory ||
            endingSlowMotion ||
            isTransitioning ||
            upgradeManager.IsChoosingUpgrade
        )
            return;

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        soundManager.PlayPauseMenuOpen();
        soundManager.PauseMusic();

        CursorManager.ShowCursor();

        isPaused = true;

        Time.timeScale = 0f;

        ui.ShowPausePanel();
    }

    private void ResumeGame()
    {
        soundManager.PlayPauseMenuClose();
        soundManager.ResumeMusic();

        isPaused = false;

        Time.timeScale = 1f;

        ui.HidePausePanel();

        pauseMenu.ResetToPause();

        if (!IsEnding)
        {
            CursorManager.HideCursor();
        }
    }

    public void LoadStatistics()
    {
        isPaused = false;
        isTransitioning = true;
        Time.timeScale = 0f;

        soundManager.PauseMusic();


        runStatsManager.OnDefeatLevelCompleted();

        SlideShowManager.Instance.PlaySingleSlide(
            CinematicLibrary.Instance.abandonedRun,
            "Statistics"
        );
    }

    public void LoadFailedStatistics()
    {
        isTransitioning = true;
        SlideShowManager.Instance.PlaySingleSlide(
            CinematicLibrary.Instance.failedRun,
            "Statistics"
        );
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        isTransitioning = true;
        string currentScene =
            SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            SlideShowManager.Instance.PlaySingleSlide(
                CinematicLibrary.Instance.level1Complete,
                "Level2"
            );

            return;
        }

        if (currentScene == "Level2")
        {
            SlideShowManager.Instance.PlaySingleSlide(
                CinematicLibrary.Instance.level2Complete,
                "Level3"
            );

            return;
        }

        if (currentScene == "Level3")
        {
            SlideShowManager.Instance.PlaySingleSlide(
                CinematicLibrary.Instance.level3Complete,
                "Level4"
            );

            return;
        }

        if (currentScene == "Level4")
        {
            SlideShowManager.Instance.PlaySlides(
                CinematicLibrary.Instance.finalSlides,
                "Statistics"
            );

            return;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        RunData.Instance.ResetRun();
        RunStats.Instance.ResetStats();
        SceneManager.LoadScene("Level1");
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        runStatsManager.OnDefeatLevelCompleted();

        soundManager.StopMusicSmooth();
        soundManager.PlayGameOver();

        endingWin = false;
        endingSlowMotion = true;
        slowMotionTimer = 0f;
    }

    private void Victory()
    {
        if (victory)
            return;

        victory = true;

        runStatsManager.OnVictoryLevelCompleted();

        soundManager.StopMusicSmooth();
        soundManager.PlayGameWin();

        endingWin = true;
        endingSlowMotion = true;
        slowMotionTimer = 0f;
    }

    private void UpdateEndingSlowMotion()
    {
        slowMotionTimer += Time.unscaledDeltaTime;

        float progress = slowMotionTimer / slowMotionDuration;

        Time.timeScale = Mathf.Lerp(1f, 0f, progress);

        if (progress >= 1f)
        {
            Time.timeScale = 0f;

            if (upgradeManager.IsChoosingUpgrade)
                return;

            endingSlowMotion = false;

            inputBlocker.SetActive(true);
            inputBlockTimer = inputBlockTime;
            isBlockingInput = true;

            CursorManager.ShowCursor();

            if (endingWin)
                ui.ShowVictoryPanel();
            else
                ui.ShowGameOverPanel();
        }
    }
}