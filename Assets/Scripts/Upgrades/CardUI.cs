using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] TMP_Text titleText;

    private Upgrade upgrade;

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        titleText.text = upgrade.Name;
    }

    public void OnClick() => upgradeManager.UpgradeSelected(upgrade);
}