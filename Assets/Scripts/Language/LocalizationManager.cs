using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    [SerializeField] private LocalizationTable table;

    private Dictionary<string, LocalizationEntry> entries = new();
    private readonly List<LocalizedText> localizedTexts = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        foreach (var entry in table.entries)
        {
            if (!entries.ContainsKey(entry.key))
                entries.Add(entry.key, entry);
        }
    }

    public string GetText(string key)
    {
        if (!entries.TryGetValue(key, out LocalizationEntry entry))
            return $"MISSING_KEY: {key}";
        return SettingsData.Language == Language.English ? entry.english : entry.russian;
    }

    public void Register(LocalizedText localizedText)
    {
        if (!localizedTexts.Contains(localizedText))
            localizedTexts.Add(localizedText);
    }

    public void Unregister(LocalizedText localizedText)
    {
        localizedTexts.Remove(localizedText);
    }

    public void RefreshAll()
    {
        foreach (var text in localizedTexts)
            text.Refresh();
    }
}