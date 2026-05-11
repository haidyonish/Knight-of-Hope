using System.Collections.Generic;
using UnityEngine;

public class RunData : MonoBehaviour
{
    public static RunData Instance;

    public int playerLevel = 1;
    public float currentXP = 0f;

    public List<UpgradeSaveData> upgrades = new List<UpgradeSaveData>();

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

    public void ResetRun()
    {
        playerLevel = 1;
        currentXP = 0f;
        upgrades.Clear();
    }
}