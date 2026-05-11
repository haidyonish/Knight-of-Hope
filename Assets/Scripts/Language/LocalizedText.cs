using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;

    private TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (LocalizationManager.Instance == null)
            return;

        LocalizationManager.Instance.Register(this);

        Refresh();
    }

    private void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.Unregister(this);
    }

    public void Refresh()
    {
        if (LocalizationManager.Instance == null)
            return;

        textComponent.text = LocalizationManager.Instance.GetText(key);
    }
}