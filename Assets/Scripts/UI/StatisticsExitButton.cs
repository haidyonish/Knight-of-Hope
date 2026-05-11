using UnityEngine;

public class StatisticsExitButton : MonoBehaviour
{
    [SerializeField] private RunStatsManager runStatsManager;

    private bool loading;

    public async void OnClickExit()
    {
        if (loading)
            return;

        loading = true;

        await runStatsManager.SubmitScoreAsync();

        SlideShowManager.Instance.FadeToScene(
            "MainMenu"
        );
    }
}