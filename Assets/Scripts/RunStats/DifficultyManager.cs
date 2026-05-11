using UnityEngine;

public static class DifficultyManager
{
    private const string DifficultyKey = "Difficulty";

    public static Difficulty CurrentDifficulty
    {
        get => (Difficulty)PlayerPrefs.GetInt(
            DifficultyKey,
            (int)Difficulty.Wanderer
        );

        set
        {
            PlayerPrefs.SetInt(
                DifficultyKey,
                (int)value
            );

            PlayerPrefs.Save();
        }
    }

    public static bool IsEasy =>
        CurrentDifficulty == Difficulty.Wanderer;

    public static float ScoreMultiplier =>
        IsEasy ? 0.5f : 1f;

    public static float BonusSwordDamage =>
        IsEasy ? 1f : 0f;

    public static float BonusDaggerDamage =>
        IsEasy ? 1f : 0f;

    public static float BonusMoveSpeed =>
        IsEasy ? 0.5f : 0f;

    public static float BonusExperienceMultiplier =>
        IsEasy ? 0.30f : 0f;
}