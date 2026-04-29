using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticsExitButton : MonoBehaviour
{
    [SerializeField] private RunStatsManager runStatsManager;

    private bool _loading;

    public async void OnClickExit()
    {
        if (_loading)
            return;

        _loading = true;

        await runStatsManager.SubmitScoreAsync();

        SceneManager.LoadScene("MainMenu");
    }
}