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

    [SerializeField] private float levelDuration = 30f;

    public float CurrentTime { get; private set; }

    private bool gameOver = false;
    private bool victory = false;
    private bool isPaused = false;
    private bool endingSlowMotion = false;
    private bool endingWin = false;
    private float slowMotionDuration = 1f;
    private float slowMotionTimer = 0f;

    public float LevelDuration => levelDuration;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
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
        if (gameOver || victory || upgradeManager.IsChoosingUpgrade)
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
    }

    public void LoadStatistics()
    {
        runStatsManager.OnDefeatLevelCompleted();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Statistics");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
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
            endingSlowMotion = false;

            if (endingWin)
                ui.ShowVictoryPanel();
            else
                ui.ShowGameOverPanel();
        }
    }
}