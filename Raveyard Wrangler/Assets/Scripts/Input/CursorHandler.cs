using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    // COMMON INTERFACE
    CommonInterface commonInterface;

    // EDITOR VARIABLES
    Texture2D sprite;

    public float cursorDistance;

    // CODE VARIABLES
    Vector2 position;

    int cursorSize;

    bool isVisible;

    void Awake()
    {
        commonInterface = CommonInterface.Instance;
        commonInterface.ReloadSettingsEvent.AddListener(ReloadSettings);

        Cursor.visible = false;
    }

    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint) && isVisible)
        {
            Graphics.DrawTexture(new(position.x - cursorSize / 2, (Screen.height - position.y) - cursorSize / 2, cursorSize, cursorSize), sprite);
        }
    }

    public void SetCursorPosition(Vector2 newPos)
    {
        position = newPos;
    }

    public void SetCursorDirection(Vector2 direction)
    {
        position = direction.normalized * cursorDistance;
    }

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }

    void ReloadSettings()
    {
        GetImageFromPath(commonInterface.settingsManager.GetSetting<string>("cursorTexturePath"));

        cursorSize = commonInterface.settingsManager.GetSetting<int>("cursorPixelSize");
    }

    void GetImageFromPath(string path)
    {
        byte[] imageBytes = File.ReadAllBytes(path);
        sprite.LoadImage(imageBytes);
    }
}
