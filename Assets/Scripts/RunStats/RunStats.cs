using UnityEngine;

public class RunStats : MonoBehaviour
{
    public static RunStats Instance;

    public float totalTime;

    public int levelsCompleted;
    public int noDamageLevels;

    public int heartsSaved;
    public int playerLevel;

    public float enemyKillSpeedBonus;

    public float scoreMultiplier = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ResetStats()
    {
        totalTime = 0f;

        levelsCompleted = 0;
        noDamageLevels = 0;

        heartsSaved = 0;

        playerLevel = 1;

        enemyKillSpeedBonus = 0f;

        scoreMultiplier = 1f;
    }

    public int CalculateTotalScore()
    {
        int score = 0;

        score += Mathf.RoundToInt(totalTime * 2f);

        score += heartsSaved * 100;

        score += levelsCompleted * 300;

        score += playerLevel * 50;

        score += Mathf.RoundToInt(enemyKillSpeedBonus * 3f);

        score += noDamageLevels * 1000;

        if (levelsCompleted == 4)
            score += 5000;

        score = Mathf.RoundToInt(score * scoreMultiplier * DifficultyManager.ScoreMultiplier);

        if (score < 0)
            score = 0;

        return score;
    }
}