using UnityEngine;

public static class SettingsData
{
    public static float Master = 1f;
    public static float Music = 1f;
    public static float SFX = 1f;
    public static float UI = 1f;

    public static void Load()
    {
        Master = PlayerPrefs.GetFloat("Master", 1f);
        Music = PlayerPrefs.GetFloat("Music", 1f);
        SFX = PlayerPrefs.GetFloat("SFX", 1f);
        UI = PlayerPrefs.GetFloat("UI", 1f);
    }

    public static float ToVolume(float value)
    {
        return value * value;
    }
}