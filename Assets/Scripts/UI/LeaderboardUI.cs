using Leadr.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TMP_Text playerRankText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerScoreText;

    [Header("Refs")]
    [SerializeField] private LeaderboardService leaderboardService;
    [SerializeField] private Transform contentParent;
    [SerializeField] private LeaderboardItemUI itemPrefab;

    [Header("Settings")]
    [SerializeField] private int topCount = 10;
    [SerializeField] private float requestCooldown = 0.5f;

    private readonly List<GameObject> _items = new();
    private bool isLoading = false;
    private float lastRequestTime = -10f;

    public async void Refresh()
    {
        if (isLoading)
            return;
        if (Time.unscaledTime - lastRequestTime < requestCooldown)
            return;
        lastRequestTime = Time.unscaledTime;
        isLoading = true;
        Clear();
        var scores = await leaderboardService.GetTopAsync(topCount);
        if (!this || !gameObject.activeInHierarchy)
        {
            isLoading = false;
            return;
        }
        if (scores == null)
        {
            isLoading = false;
            return;
        }
        foreach (var score in scores)
        {
            var item = Instantiate(itemPrefab, contentParent);
            item.Setup(score.Rank, score.PlayerName, (long)score.Value);
            _items.Add(item.gameObject);
        }
        await LoadPlayerScore();
        isLoading = false;
    }

    private async System.Threading.Tasks.Task LoadPlayerScore()
    {
        var myScore = await leaderboardService.GetMyScoreAsync();
        if (!this || !gameObject.activeInHierarchy)
            return;
        if (myScore == null)
        {
            playerRankText.text = "-";
            playerNameText.text = string.IsNullOrEmpty(PlayerProfile.PlayerName) ? "Unknown" : PlayerProfile.PlayerName;
            playerScoreText.text = PlayerProfile.BestScore.ToString();
            return;
        }
        playerRankText.text = $"{myScore.Rank}";
        playerNameText.text = myScore.PlayerName;
        playerScoreText.text = myScore.Value.ToString();
    }

    private void Clear()
    {
        foreach (var item in _items)
        {
            if (item)
                Destroy(item);
        }
        _items.Clear();
    }

    private void OnDisable()
    {
        Clear();
        isLoading = false;
    }
}