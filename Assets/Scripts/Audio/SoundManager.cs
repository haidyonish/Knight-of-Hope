using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

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

    [Header("UI")]
    [SerializeField] private SoundData buttonClick;
    [SerializeField] private SoundData buttonHover;
    [SerializeField] private SoundData cardHover;
    [SerializeField] private SoundData cardSelected;
    [SerializeField] private SoundData pauseMenuClose;
    [SerializeField] private SoundData pauseMenuOpen;

    private void Play(SoundData sound)
    {
        if (sound.clip == null)
            return;

        audioSource.PlayOneShot(sound.clip, sound.volume);
    }

    // SFX
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

    // UI
    public void PlayButtonClick() => Play(buttonClick);
    public void PlayButtonHover() => Play(buttonHover);
    public void PlayCardHover() => Play(cardHover);
    public void PlayCardSelected() => Play(cardSelected);
    public void PlayPauseMenuClose() => Play(pauseMenuClose);
    public void PlayPauseMenuOpen() => Play(pauseMenuOpen);
}