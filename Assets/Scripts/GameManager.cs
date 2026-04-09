using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UI ui;

    [SerializeField] private float levelDuration = 30.0f;
    public float CurrentTime { get; private set; }
    private bool gameOver = false;

    public float LevelDuration => levelDuration;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > levelDuration && !gameOver)
            Victory();
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void GameOver()
    {
        if (gameOver) return;

        ui.ShowGameOverPanel();
        gameOver = true;
        Time.timeScale = 0.3f;
    }

    private void Victory()
    {
        ui.ShowVictoryPanel();
        Time.timeScale = 0.3f;
    }
}
