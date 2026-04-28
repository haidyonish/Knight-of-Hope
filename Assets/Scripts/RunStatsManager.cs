using UnityEngine;

public class RunStatsManager : MonoBehaviour
{
    [SerializeField] private VIPHealth vipHealth;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private GameManager gameManager;

    public void OnVictoryLevelCompleted()
    {
        RunStats.Instance.playerLevel = playerXP.Level;
        RunStats.Instance.levelsCompleted++;
        RunStats.Instance.totalTime += gameManager.LevelDuration;
        RunStats.Instance.heartsLost += Mathf.RoundToInt(vipHealth.MaxHealth - vipHealth.CurrentHealth);
        if (vipHealth.MaxHealth == vipHealth.CurrentHealth)
            RunStats.Instance.noDamageLevels++;
    }

    public void OnDefeatLevelCompleted()
    {
        RunStats.Instance.playerLevel = playerXP.Level;
        RunStats.Instance.totalTime += gameManager.CurrentTime;
        RunStats.Instance.heartsLost += Mathf.RoundToInt(vipHealth.MaxHealth - vipHealth.CurrentHealth);
    }

    public void AddEnemyKillTimePenalty(float value) => RunStats.Instance.enemyKillTimePenalty += value;

    public int CalculateScore()
    {
        int score = 0;

        score += Mathf.RoundToInt(RunStats.Instance.totalTime * 2f);
        score -= RunStats.Instance.heartsLost * 100;
        score += RunStats.Instance.levelsCompleted * 300;
        score += RunStats.Instance.playerLevel * 50;
        score -= Mathf.RoundToInt(RunStats.Instance.enemyKillTimePenalty * 0.5f);

        if (RunStats.Instance.levelsCompleted == 4)
            score += 1000;

        score += RunStats.Instance.noDamageLevels * 300;

        if (score < 0) 
            score = 0;

        return score;
    }
}