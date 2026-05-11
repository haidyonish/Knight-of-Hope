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
            return false;
        }
        _isSubmitting = true;
        string playerName = PlayerProfile.PlayerName;
        var result = await LeadrClient.Instance.SubmitScoreAsync(leaderboardId, score, playerName);
        _isSubmitting = false;
        return result.IsSuccess;
    }

    public async Task<List<Score>> GetTopAsync(int count)
    {
        var result = await LeadrClient.Instance.GetScoresAsync(leaderboardId, limit: count);
        if (result.IsSuccess)
        {
            return result.Data.Items;
        }
        return null;
    }

    public async Task<Score> GetMyScoreAsync()
    {
        var result = await LeadrClient.Instance.GetMyScoresAsync(leaderboardId, 1);
        if (!result.IsSuccess)
        {
            return null;
        }
        if (result.Data.Items.Count <= 0)
            return null;
        return result.Data.Items[0];
    }
}