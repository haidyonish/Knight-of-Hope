using UnityEngine;

public class RunStats : MonoBehaviour
{
    public static RunStats Instance;

    public float totalTime;
    public int levelsCompleted;
    public int noDamageLevels;
    public int heartsLost;
    public int playerLevel;
    public float enemyKillTimePenalty;

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
        heartsLost = 0;
        playerLevel = 1;
        enemyKillTimePenalty = 0f;
        noDamageLevels = 0;
    }

    public int CalculateTotalScore()
    {
        int score = 0;

        score += Mathf.RoundToInt(totalTime * 2f);
        score -= heartsLost * 100;
        score += levelsCompleted * 300;
        score += playerLevel * 50;
        score -= Mathf.RoundToInt(enemyKillTimePenalty * 0.5f);

        if (levelsCompleted == 4)
            score += 1000;

        score += noDamageLevels * 300;

        if (score < 0)
            score = 0;

        return score;
    }
}