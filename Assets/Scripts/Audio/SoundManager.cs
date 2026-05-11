using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    private class SoundData
    {
        public AudioClip clip;
        [Range(0f, 3f)]
        public float volume = 1f;
    }

    [Header("SFX")]
    [SerializeField] private SoundData enemyDeath;
    [SerializeField] private SoundData enemyHit;
    [SerializeField] private SoundData gameOver;
    [SerializeField] private SoundData gameWin;
    [SerializeField] private SoundData jump;
    [SerializeField] private SoundData kingHurt;
    [SerializeField] private SoundData levelUp;
    [SerializeField] private SoundData playerHurt;
    [SerializeField] private SoundData princessHurt;
    [SerializeField] private SoundData princeHurt;
    [SerializeField] private SoundData queenHurt;
    [SerializeField] private SoundData swordHitEnemy;
    [SerializeField] private SoundData swordSwing;
    [SerializeField] private SoundData cardsReveal;
    [SerializeField] private SoundData daggerSwing;
    [SerializeField] private SoundData daggerHit;
    [SerializeField] private SoundData rockThrow;
    [SerializeField] private SoundData rockHit;

    [Header("UI")]
    [SerializeField] private SoundData buttonClick;
    [SerializeField] private SoundData buttonHover;
    [SerializeField] private SoundData cardHover;
    [SerializeField] private SoundData cardSelected;
    [SerializeField] private SoundData pauseMenuClose;
    [SerializeField] private SoundData pauseMenuOpen;

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Global Volume")]
    [SerializeField] private float masterVolumeMultiplier = 0.1f;
    [SerializeField] private float sfxMultiplier = 1f;

    [Header("Music")]
    [SerializeField] private float baseMusicVolume = 0.5f;
    [SerializeField] private float fadeSpeed = 2f;

    private bool musicPaused = false;
    private bool musicStopped = false;
    private float currentVolume;

    private void Awake()
    {
        SettingsData.Load();
        currentVolume = musicSource.volume;
    }

    private void Update()
    {
        float target = baseMusicVolume * masterVolumeMultiplier * SettingsData.ToVolume(SettingsData.Master) * SettingsData.ToVolume(SettingsData.Music);
        if (musicPaused)
            target *= 0.2f;
        if (musicStopped)
            target = 0f;
        currentVolume = Mathf.MoveTowards(currentVolume, target, fadeSpeed * Time.unscaledDeltaTime);
        musicSource.volume = currentVolume;
    }

    public void PauseMusic()
    {
        musicPaused = true;
    }

    public void ResumeMusic()
    {
        musicPaused = false;
    }

    public void StopMusicSmooth()
    {
        musicStopped = true;
    }

    private void Play(SoundData sound, bool isUI = false)
    {
        if (sound.clip == null)
            return;
        float category = isUI ? SettingsData.UI : SettingsData.SFX;
        float volume = sound.volume * sfxMultiplier * masterVolumeMultiplier * SettingsData.ToVolume(SettingsData.Master) * SettingsData.ToVolume(category);
        audioSource.PlayOneShot(sound.clip, volume);
    }

    public void PlayEnemyDeath() => Play(enemyDeath);
    public void PlayEnemyHit() => Play(enemyHit);
    public void PlayGameOver() => Play(gameOver);
    public void PlayGameWin() => Play(gameWin);
    public void PlayJump() => Play(jump);
    public void PlayKingHurt() => Play(kingHurt);
    public void PlayLevelUp() => Play(levelUp);
    public void PlayPlayerHurt() => Play(playerHurt);
    public void PlayPrincessHurt() => Play(princessHurt);
    public void PlayPrinceHurt() => Play(princeHurt);
    public void PlayQueenHurt() => Play(queenHurt);
    public void PlaySwordHitEnemy() => Play(swordHitEnemy);
    public void PlaySwordSwing() => Play(swordSwing);
    public void PlayCardsReveal() => Play(cardsReveal);
    public void PlayDaggerSwing() => Play(daggerSwing);
    public void PlayDaggerHit() => Play(daggerHit);
    public void PlayRockThrow() => Play(rockThrow);
    public void PlayRockHit() => Play(rockHit);

    public void PlayButtonClick() => Play(buttonClick, true);
    public void PlayButtonHover() => Play(buttonHover, true);
    public void PlayCardHover() => Play(cardHover, true);
    public void PlayCardSelected() => Play(cardSelected, true);
    public void PlayPauseMenuClose() => Play(pauseMenuClose, true);
    public void PlayPauseMenuOpen() => Play(pauseMenuOpen, true);
}