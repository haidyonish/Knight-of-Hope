using UnityEngine;

public class IntroSlides : MonoBehaviour
{
    [SerializeField]
    private SlideData[] slides;

    [SerializeField]
    private string nextScene;

    public void Play()
    {
        SlideShowManager.Instance.PlaySlides(
            slides,
            nextScene
        );
    }
}