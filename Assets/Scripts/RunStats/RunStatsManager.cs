using UnityEngine;
using System.Threading.Tasks;

public class RunStatsManager : MonoBehaviour
{
    [SerializeField] private VIPHealth vipHealth;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LeaderboardService leaderboardService;

    private bool _submitted;

    public void OnVictoryLevelCompleted()
    {
        RunStats.Instance.playerLevel = playerXP.Level;

        RunStats.Instance.levelsCompleted++;

        RunStats.Instance.totalTime += gameManager.LevelDuration;

        RunStats.Instance.heartsSaved +=
            Mathf.RoundToInt(vipHealth.CurrentHealth);

        if (vipHealth.MaxHealth == vipHealth.CurrentHealth)
            RunStats.Instance.noDamageLevels++;
    }

    public void OnDefeatLevelCompleted()
    {
        RunStats.Instance.playerLevel = playerXP.Level;

        RunStats.Instance.totalTime += gameManager.CurrentTime;

        RunStats.Instance.heartsSaved +=
            Mathf.RoundToInt(vipHealth.CurrentHealth);
    }

    public void AddEnemyKillSpeedBonus(float value)
    {
        RunStats.Instance.enemyKillSpeedBonus += value;
    }

    public async Task SubmitScoreAsync()
    {
        if (_submitted)
            return;

        int score = RunStats.Instance.CalculateTotalScore();

        bool isNewBest = PlayerProfile.TrySetBestScore(score);

        if (!isNewBest)
            return;

        _submitted = true;

        await leaderboardService.SubmitScoreAsync(score);
    }
}