using UnityEngine;
using UnityEngine.InputSystem;

public static class CursorManager
{
    private static UICursor uiCursor;

    public static bool IsGameplayCursorLocked { get; private set; }

    public static void Register(UICursor cursor)
    {
        uiCursor = cursor;

        if (IsGameplayCursorLocked)
            uiCursor.Hide();
        else
            uiCursor.Show();
    }

    public static void Unregister(UICursor cursor)
    {
        if (uiCursor == cursor)
            uiCursor = null;
    }

    public static void HideCursor()
    {
        IsGameplayCursorLocked = true;

        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Mouse.current?.WarpCursorPosition(center);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        uiCursor?.Hide();
    }

    public static void ShowCursor()
    {
        IsGameplayCursorLocked = false;

        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Mouse.current?.WarpCursorPosition(center);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        uiCursor?.Show();
    }
}