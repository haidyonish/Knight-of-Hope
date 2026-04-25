using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Image cardImage;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text levelText;

    private Upgrade upgrade;

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;

        cardImage.sprite = upgrade.CardSprite;

        titleText.text = upgrade.Name;
        descriptionText.text = upgrade.Description;
        levelText.text = $"{upgrade.Level} из {upgrade.MaxLevel}";
    }

    public void OnClick()
    {
        upgradeManager.UpgradeSelected(upgrade);
    }
}