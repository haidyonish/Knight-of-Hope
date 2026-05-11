using UnityEngine;

public class CinematicLibrary : MonoBehaviour
{
    public static CinematicLibrary Instance;

    [Header("Level Complete")]
    public SlideData level1Complete;
    public SlideData level2Complete;
    public SlideData level3Complete;

    [Header("Final")]
    public SlideData[] finalSlides;

    [Header("Failure")]
    public SlideData failedRun;

    [Header("Abandoned")]
    public SlideData abandonedRun;

    private void Awake()
    {
        Instance = this;
    }
}