using UnityEngine;

public class SecretScrollUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}