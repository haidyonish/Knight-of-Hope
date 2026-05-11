using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject pausePanel;

    private void Update()
    {
        UpdateTimer();
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowGameOverPanel() => gameOverPanel.SetActive(true);

    public void ShowVictoryPanel() => victoryPanel.SetActive(true);

    public void UpdateLevelText(int level)
    {
        string levelWord = LocalizationManager.Instance.GetText("ui_level");
        levelText.text = $"{levelWord} {level}";
    }

    private void UpdateTimer()
    {
        float remainingTime = Mathf.Max(gameManager.LevelDuration - gameManager.CurrentTime, 0f);
        int minutes = (int)(remainingTime / 60);
        int seconds = (int)(remainingTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}