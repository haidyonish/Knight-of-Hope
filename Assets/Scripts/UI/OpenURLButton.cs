using UnityEngine;

public class OpenURLButton : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}