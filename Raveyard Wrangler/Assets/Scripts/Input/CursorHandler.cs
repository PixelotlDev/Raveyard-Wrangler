using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    // GAME MANAGER
    GameManager gameManager = GameManager.instance;

    // EDITOR VARIABLES
    [SerializeField]
    Texture sprite;

    [SerializeField]
    float cursorDistance;

    // CODE VARIABLES
    Vector2 position;

    int cursorSize;

    bool isVisible;

    void Awake()
    {
        gameManager.ReloadSettingsEvent.AddListener(LoadSettings);

        LoadSettings();
        
        // Turn the system cursor off
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

    void LoadSettings()
    {
        cursorSize = gameManager.settingsManager.GetSetting<int>("cursorPixelSize");
    }
}
