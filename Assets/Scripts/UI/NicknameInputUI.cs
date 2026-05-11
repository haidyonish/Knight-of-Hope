using TMPro;
using UnityEngine;

public class NicknameInputUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        messageText.text = LocalizationManager.Instance.GetText("nickname_input_default");
        inputField.characterLimit = 16;
    }

    public void Confirm()
    {
        string input = inputField.text;
        if (!NicknameValidator.TryValidate(input, out string errorKey, out string clean))
        {
            messageText.text = LocalizationManager.Instance.GetText(errorKey);
            return;
        }
        PlayerProfile.SetName(clean);
        panel.SetActive(false);
    }
}