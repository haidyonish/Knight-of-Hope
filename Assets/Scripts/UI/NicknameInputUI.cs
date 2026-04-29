using TMPro;
using UnityEngine;

public class NicknameInputUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject panel;

    [Header("Texts")]
    [SerializeField]
    private string defaultMessage =
        "Введите имя, которое будет отображаться в таблице лидеров";

    private void Start()
    {
        messageText.text = defaultMessage;
        inputField.characterLimit = 16;
    }

    public void Confirm()
    {
        string input = inputField.text;

        if (!NicknameValidator.TryValidate(input, out string error, out string clean))
        {
            messageText.text = error;
            return;
        }

        PlayerProfile.SetName(clean);

        panel.SetActive(false);
    }
}