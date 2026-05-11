using UnityEngine;
using TMPro;

public class StatisticsUI : MonoBehaviour
{
    [System.Serializable]
    public class StatLine
    {
        public TMP_Text label;
        public TMP_Text value;
        public TMP_Text score;
    }

    [Header("Lines")]
    [SerializeField] private StatLine[] lines;

    [Header("Total")]
    [SerializeField] private TMP_Text totalScoreText;

    [Header("Refs")]
    [SerializeField] private RunStatsManager runStatsManager;

    [Header("Timing")]
    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private float showDelay = 0.25f;
    [SerializeField] private float countDuration = 0.6f;

    [Header("Total Animation")]
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmplitude = 0.1f;

    private bool pulseActive = false;
    private float pulseTimer = 0f;
    private Vector3 baseScale;

    private int stepIndex = 0;
    private float timer = 0f;

    private int totalScore = 0;

    private bool showing = false;
    private bool counting = false;
    private bool submitted = false;

    private float countTimer = 0f;
    private int startValue = 0;
    private int targetValue = 0;

    private void Start()
    {
        baseScale = totalScoreText.transform.localScale;
        SubmitScoreSafe();
        SetupTexts();
        HideAll();
        totalScoreText.text = "0";
        timer = startDelay;
    }

    private void Update()
    {
        UpdateTotalPulse();
        if (stepIndex >= lines.Length)
        {
            pulseActive = true;
            return;
        }
        if (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            return;
        }
        if (counting)
        {
            countTimer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(countTimer / countDuration);
            int value = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            lines[stepIndex].score.text = Format(value);
            totalScoreText.text = (totalScore + value).ToString();
            if (t >= 1f)
            {
                counting = false;
                totalScore += targetValue;
                totalScoreText.text = totalScore.ToString();
                stepIndex++;
                timer = showDelay;
            }
            return;
        }
        if (!showing)
        {
            ShowLine(stepIndex);
            showing = true;
            timer = showDelay;
            return;
        }
        StartCounting();
        showing = false;
    }

    private async void SubmitScoreSafe()
    {
        if (submitted)
            return;
        submitted = true;
        await runStatsManager.SubmitScoreAsync();
    }

    private void UpdateTotalPulse()
    {
        if (!pulseActive)
            return;
        pulseTimer += Time.unscaledDeltaTime * pulseSpeed;
        float scale = 1f + Mathf.Sin(pulseTimer) * pulseAmplitude;
        totalScoreText.transform.localScale = baseScale * scale;
    }

    private void StartCounting()
    {
        startValue = 0;
        targetValue = GetScoreForLine(stepIndex);
        countTimer = 0f;
        counting = true;
    }

    private int GetScoreForLine(int index)
    {
        var stats = RunStats.Instance;
        switch (index)
        {
            case 0:
                return stats.levelsCompleted * 300;
            case 1:
                return Mathf.RoundToInt(stats.totalTime * 2f);
            case 2:
                return stats.heartsSaved * 100;
            case 3:
                return stats.playerLevel * 50;
            case 4:
                return Mathf.RoundToInt(stats.enemyKillSpeedBonus * 3f);
            case 5:
                return stats.noDamageLevels * 1000;
            case 6:
                return stats.levelsCompleted == 4 ? 5000 : 0;
            case 7:
                {
                    int baseScore = 0;
                    baseScore += Mathf.RoundToInt(stats.totalTime * 2f);
                    baseScore += stats.heartsSaved * 100;
                    baseScore += stats.levelsCompleted * 300;
                    baseScore += stats.playerLevel * 50;
                    baseScore += Mathf.RoundToInt(stats.enemyKillSpeedBonus * 3f);
                    baseScore += stats.noDamageLevels * 1000;
                    if (stats.levelsCompleted == 4)
                        baseScore += 5000;
                    return Mathf.RoundToInt(baseScore * (stats.scoreMultiplier - 1f));
                }
            case 8:
                {
                    if (DifficultyManager.ScoreMultiplier >= 1f)
                        return 0;
                    int currentScore = RunStats.Instance.CalculateTotalScore();
                    int fullScore = Mathf.RoundToInt(currentScore / DifficultyManager.ScoreMultiplier);
                    return currentScore - fullScore;
                }
        }
        return 0;
    }

    private void SetupTexts()
    {
        var stats = RunStats.Instance;
        SetLine(0, LocalizationManager.Instance.GetText("stats_levels_completed"), stats.levelsCompleted.ToString());
        SetLine(1, LocalizationManager.Instance.GetText("stats_total_time"), Mathf.RoundToInt(stats.totalTime) + "s");
        SetLine(2, LocalizationManager.Instance.GetText("stats_hearts_saved"), stats.heartsSaved.ToString());
        SetLine(3, LocalizationManager.Instance.GetText("stats_player_level"), stats.playerLevel.ToString());
        SetLine(4, LocalizationManager.Instance.GetText("stats_clear_speed"), Mathf.RoundToInt(stats.enemyKillSpeedBonus).ToString());
        SetLine(5, LocalizationManager.Instance.GetText("stats_no_damage_levels"), stats.noDamageLevels.ToString());
        SetLine(6, LocalizationManager.Instance.GetText("stats_completion_bonus"), stats.levelsCompleted == 4 ? LocalizationManager.Instance.GetText("common_yes") : LocalizationManager.Instance.GetText("common_no"));
        SetLine(7, LocalizationManager.Instance.GetText("stats_ambition"), "x" + stats.scoreMultiplier.ToString("0.00"));
        SetLine(8, LocalizationManager.Instance.GetText("stats_difficulty_penalty"), DifficultyManager.IsEasy ? LocalizationManager.Instance.GetText("difficulty_wanderer") : "-");
    }

    private void SetLine(int index, string label, string value)
    {
        lines[index].label.text = label;
        lines[index].value.text = value;
        lines[index].score.text = "";
    }

    private void HideAll()
    {
        foreach (var line in lines)
        {
            line.label.gameObject.SetActive(false);
            line.value.gameObject.SetActive(false);
            line.score.gameObject.SetActive(false);
        }
    }

    private void ShowLine(int index)
    {
        lines[index].label.gameObject.SetActive(true);
        lines[index].value.gameObject.SetActive(true);
        lines[index].score.gameObject.SetActive(true);
    }

    private string Format(int value)
    {
        return value > 0 ? "+" + value : value.ToString();
    }
}