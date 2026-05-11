using UnityEngine;
using UnityEngine.InputSystem;

public class UICursor : MonoBehaviour
{
    [SerializeField] private RectTransform cursorRect;

    private Vector2 screenCenter;

    private void Awake()
    {
        Cursor.visible = false;

        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        cursorRect.position = screenCenter;

        CursorManager.Register(this);
    }

    private void OnDestroy()
    {
        CursorManager.Unregister(this);
    }

    private void Update()
    {
        if (CursorManager.IsGameplayCursorLocked)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        cursorRect.position = mousePosition;
    }

    public void Show()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        cursorRect.position = screenCenter;

        cursorRect.gameObject.SetActive(true);
    }

    public void Hide()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        cursorRect.position = screenCenter;

        cursorRect.gameObject.SetActive(false);
    }
}