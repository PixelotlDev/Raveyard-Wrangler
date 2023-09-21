using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    // EDITOR VARIABLES
    [SerializeField]
    Texture sprite;

    public float cursorDistance;

    // CODE VARIABLES
    Vector2 position;

    bool isVisible;

    void Awake()
    {
        Cursor.visible = false;
    }

    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint) && isVisible)
        {
            Graphics.DrawTexture(new(position.x - sprite.width / 2, (Screen.height - position.y) - sprite.height / 2, sprite.width, sprite.height), sprite);
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
}
