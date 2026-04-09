using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] Slider XPBar;

    private void Update()
    {
        UpdateTimer();
    }

    public void ShowGameOverPanel() => gameOverPanel.SetActive(true);
    public void ShowVictoryPanel() => victoryPanel.SetActive(true);

    public void UpdateXPBar(float currentXP, float maxXP)
    {
        XPBar.maxValue = maxXP;
        XPBar.value = currentXP;
    }

    private void UpdateTimer()
    {
        float remainingTime = Mathf.Max(gameManager.LevelDuration - gameManager.CurrentTime, 0f);
        int minutes = (int)(remainingTime / 60);
        int seconds = (int)(remainingTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
