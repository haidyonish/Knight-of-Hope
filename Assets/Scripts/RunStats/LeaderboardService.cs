using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Leadr;
using Leadr.Models;

public class LeaderboardService : MonoBehaviour
{
    [SerializeField] private string leaderboardId;

    private bool _isSubmitting;

    public async Task<bool> SubmitScoreAsync(int score)
    {
        if (_isSubmitting)
        {
            Debug.LogWarning("[Leaderboard] Already submitting");
            return false;
        }

        _isSubmitting = true;

        string playerName = PlayerProfile.PlayerName;

        Debug.Log($"[Leaderboard] Submit: {playerName} - {score}");

        var result = await LeadrClient.Instance.SubmitScoreAsync(
            leaderboardId,
            score,
            playerName
        );

        _isSubmitting = false;

        if (result.IsSuccess)
        {
            Debug.Log($"[Leaderboard] Success! Rank: {result.Data.Rank}");
            return true;
        }
        else
        {
            Debug.LogError($"[Leaderboard] Failed: {result.Error.Message}");
            return false;
        }
    }

    public async Task<List<Score>> GetTopAsync(int count)
    {
        var result = await LeadrClient.Instance.GetScoresAsync(
            leaderboardId,
            limit: count
        );

        if (result.IsSuccess)
        {
            Debug.Log("[Leaderboard] Top loaded");
            return result.Data.Items;
        }

        Debug.LogError($"[Leaderboard] Load failed: {result.Error.Message}");
        return null;
    }
}