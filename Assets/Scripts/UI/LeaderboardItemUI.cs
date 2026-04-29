using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Background")]
    [SerializeField] private Image background;

    [Header("Colors")]
    [SerializeField] private Color normalColor = new Color(1, 1, 1, 0f);

    [SerializeField] private Color goldColor = new Color(1f, 0.84f, 0f, 0.25f);
    [SerializeField] private Color silverColor = new Color(0.75f, 0.75f, 0.75f, 0.25f);
    [SerializeField] private Color bronzeColor = new Color(0.8f, 0.5f, 0.2f, 0.25f);

    [SerializeField] private Color playerColor = new Color(0.3f, 1f, 0.4f, 0.25f);

    [Header("Scale Animation")]
    [SerializeField] private float pulseSpeed = 1.5f;
    [SerializeField] private float pulseAmount = 0.05f;

    private bool isPlayer;
    private Color baseColor;

    private float pulseTimer;
    private Vector3 baseScale;

    public void Setup(int rank, string playerName, long score)
    {
        rankText.text = $"{rank}";
        nameText.text = playerName;
        scoreText.text = score.ToString();

        baseScale = nameText.transform.localScale;

        isPlayer =
            playerName == PlayerProfile.PlayerName &&
            score == PlayerProfile.BestScore;

        if (isPlayer)
        {
            baseColor = playerColor;
        }
        else
        {
            switch (rank)
            {
                case 1:
                    baseColor = goldColor;
                    break;
                case 2:
                    baseColor = silverColor;
                    break;
                case 3:
                    baseColor = bronzeColor;
                    break;
                default:
                    baseColor = normalColor;
                    break;
            }
        }

        background.color = baseColor;
    }

    private void Update()
    {
        if (!isPlayer)
            return;

        pulseTimer += Time.unscaledDeltaTime * pulseSpeed;

        float scale = 1f + Mathf.Sin(pulseTimer) * pulseAmount;

        nameText.transform.localScale = baseScale * scale;
    }
}