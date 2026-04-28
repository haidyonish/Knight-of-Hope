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
}