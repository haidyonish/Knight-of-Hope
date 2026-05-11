using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class SlideShowManager : MonoBehaviour
{
    public static SlideShowManager Instance;

    [Header("Images")]
    [SerializeField] private CanvasGroup rootCanvasGroup;

    [SerializeField] private Image backImage;
    [SerializeField] private Image frontImage;

    [Header("Text")]
    [SerializeField] private CanvasGroup textCanvasGroup;

    [SerializeField] private TMP_Text slideText;
    [SerializeField] private TMP_Text skipText;

    [Header("Timing")]
    [SerializeField] private float imageFadeDuration = 2f;

    [SerializeField] private float textFadeDuration = 0.35f;

    [SerializeField] private float sceneFadeDuration = 1.5f;

    [Header("Skip")]
    [SerializeField] private bool allowSkip = true;

    [SerializeField] private float skipHoldDuration = 1f;

    private bool isPlaying = false;
    private bool skipRequested = false;

    private float skipTimer = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        ResetVisualState();
    }

    private void Update()
    {
        if (!isPlaying || !allowSkip)
            return;

        if (Keyboard.current.spaceKey.isPressed)
        {
            skipTimer += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(skipTimer / skipHoldDuration);

            int percent = Mathf.RoundToInt(progress * 100f);

            skipText.text = LocalizationManager.Instance.GetText("intro_skip") + $" {percent}%";

            if (skipTimer >= skipHoldDuration)
            {
                skipRequested = true;

                skipText.text = LocalizationManager.Instance.GetText("intro_skipping");
            }
        }
        else
        {
            if (skipRequested)
                return;

            skipTimer = 0f;

            skipText.text = LocalizationManager.Instance.GetText("intro_skip");
        }
    }

    public void PlaySlides(SlideData[] slides, string nextScene)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        StartCoroutine(PlaySlidesRoutine(slides, nextScene));
    }

    private IEnumerator PlaySlidesRoutine(SlideData[] slides, string nextScene)
    {
        ResetVisualState();

        isPlaying = true;

        skipRequested = false;
        skipTimer = 0f;

        rootCanvasGroup.interactable = true;
        rootCanvasGroup.blocksRaycasts = true;

        Time.timeScale = 0f;

        if (slides.Length <= 0)
            yield break;

        backImage.sprite = slides[0].image;

        slideText.text = LocalizationManager.Instance.GetText(slides[0].localizationKey);

        yield return FadeRoot(0f, 1f, imageFadeDuration);

        yield return FadeCanvasGroup(textCanvasGroup, 0f, 1f, textFadeDuration);

        yield return WaitSlideDuration(slides[0].duration);

        for (int i = 1; i < slides.Length; i++)
        {
            if (skipRequested)
                break;

            SlideData slide = slides[i];

            frontImage.sprite = slide.image;

            SetImageAlpha(frontImage, 0f);

            yield return FadeCanvasGroup(textCanvasGroup, 1f, 0f, textFadeDuration);

            slideText.text = LocalizationManager.Instance.GetText(slide.localizationKey);

            yield return FadeImage(frontImage, 0f, 1f);

            yield return FadeCanvasGroup(textCanvasGroup, 0f, 1f, textFadeDuration);

            yield return WaitSlideDuration(slide.duration);

            backImage.sprite = frontImage.sprite;

            SetImageAlpha(frontImage, 0f);
        }

        yield return FadeCanvasGroup(textCanvasGroup, 1f, 0f, textFadeDuration);

        yield return FadeImage(backImage, 1f, 0f);

        Time.timeScale = 1f;

        yield return SceneManager.LoadSceneAsync(nextScene);


        yield return null;
        yield return null;

        isPlaying = false;

        skipText.text = "";

        textCanvasGroup.alpha = 0f;

        yield return FadeRoot(1f, 0f, sceneFadeDuration);

        FinishPlayback();
    }

    public void PlaySingleSlide(Sprite image, string textKey, string nextScene, float duration = 2f)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        StartCoroutine(PlaySingleSlideRoutine(image, textKey, nextScene, duration));
    }

    public void PlaySingleSlide(SlideData slide, string nextScene)
    {
        PlaySingleSlide(slide.image, slide.localizationKey, nextScene, slide.duration);
    }

    private IEnumerator PlaySingleSlideRoutine(Sprite image, string textKey, string nextScene, float duration)
    {
        ResetVisualState();

        isPlaying = true;

        skipRequested = false;
        skipTimer = 0f;

        rootCanvasGroup.interactable = true;
        rootCanvasGroup.blocksRaycasts = true;

        backImage.sprite = image;

        SetImageAlpha(backImage, 1f);
        SetImageAlpha(frontImage, 0f);

        slideText.text = LocalizationManager.Instance.GetText(textKey);

        yield return FadeRoot(0f, 1f, imageFadeDuration);

        yield return FadeCanvasGroup(textCanvasGroup, 0f, 1f, textFadeDuration);

        yield return WaitSlideDuration(duration);

        yield return FadeCanvasGroup(textCanvasGroup, 1f, 0f, textFadeDuration);

        yield return FadeImage(backImage, 1f, 0f);

        Time.timeScale = 1f;

        yield return SceneManager.LoadSceneAsync(nextScene);

        yield return null;
        yield return null;

        isPlaying = false;

        skipText.text = "";

        textCanvasGroup.alpha = 0f;

        yield return FadeRoot(1f, 0f, sceneFadeDuration);

        FinishPlayback();
    }

    private void FinishPlayback()
    {
        rootCanvasGroup.interactable = false;
        rootCanvasGroup.blocksRaycasts = false;

        isPlaying = false;

        ResetVisualState();
    }

    private void ResetVisualState()
    {
        rootCanvasGroup.alpha = 0f;

        rootCanvasGroup.interactable = false;
        rootCanvasGroup.blocksRaycasts = false;

        SetImageAlpha(backImage, 1f);
        SetImageAlpha(frontImage, 0f);

        backImage.sprite = null;
        frontImage.sprite = null;

        textCanvasGroup.alpha = 0f;
    }

    private IEnumerator WaitSlideDuration(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            if (skipRequested)
                yield break;

            timer += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeImage(Image image, float from, float to)
    {
        float timer = 0f;

        while (timer < imageFadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / imageFadeDuration;

            float alpha = Mathf.Lerp(from, to, t);

            SetImageAlpha(image, alpha);

            yield return null;
        }

        SetImageAlpha(image, to);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / duration;

            group.alpha = Mathf.Lerp(from, to, t);

            yield return null;
        }

        group.alpha = to;
    }

    private IEnumerator FadeRoot(float from, float to, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / duration;

            t = Mathf.Pow(t, 3f);

            rootCanvasGroup.alpha = Mathf.Lerp(from, to, t);

            yield return null;
        }

        rootCanvasGroup.alpha = to;
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;

        color.a = alpha;

        image.color = color;
    }

    public void FadeToScene(string nextScene)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        StartCoroutine(FadeToSceneRoutine(nextScene));
    }

    private IEnumerator FadeToSceneRoutine(string nextScene)
    {
        ResetVisualState();

        rootCanvasGroup.interactable = true;
        rootCanvasGroup.blocksRaycasts = true;

        backImage.sprite = null;
        frontImage.sprite = null;

        SetImageAlpha(backImage, 0f);
        SetImageAlpha(frontImage, 0f);

        slideText.text = "";
        skipText.text = "";

        textCanvasGroup.alpha = 0f;

        yield return FadeRootSmooth(0f, 1f, 0.6f);

        yield return SceneManager.LoadSceneAsync(nextScene);

        yield return null;

        yield return FadeRoot(1f, 0f, 0.8f);

        FinishPlayback();
    }

    private IEnumerator FadeRootSmooth(float from, float to, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            rootCanvasGroup.alpha = Mathf.Lerp(from, to, t);

            yield return null;
        }

        rootCanvasGroup.alpha = to;
    }
}