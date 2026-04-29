using UnityEngine;

public static class PlayerProfile
{
    private const string NameKey = "PlayerName";
    private const string BestScoreKey = "BestScore";

    private static bool _loaded;

    private static string _playerName;
    private static int _bestScore;

    public static string PlayerName
    {
        get
        {
            if (!_loaded)
                Load();

            return _playerName;
        }
        private set => _playerName = value;
    }

    public static int BestScore
    {
        get
        {
            if (!_loaded)
                Load();

            return _bestScore;
        }
        private set => _bestScore = value;
    }

    public static bool HasName => !string.IsNullOrEmpty(PlayerName);

    public static void Load()
    {
        _playerName = PlayerPrefs.GetString(NameKey, "");
        _bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        _loaded = true;
    }

    public static void SetName(string name)
    {
        PlayerName = name;

        PlayerPrefs.SetString(NameKey, name);
        PlayerPrefs.Save();
    }

    public static bool TrySetBestScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;

            PlayerPrefs.SetInt(BestScoreKey, score);
            PlayerPrefs.Save();

            return true;
        }

        return false;
    }
}