using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerActions actions;

    [SerializeField]
    MovementController playerController;
    [SerializeField]
    WeaponManager weapon;
    [SerializeField]
    CursorHandler cursor;

    [SerializeField]
    Camera cam;

    void Awake()
    {
        actions = new PlayerActions();
    }

    void Update()
    {
        // MOVE PLAYER
        Vector2 movementVect = actions.gameplay.move.ReadValue<Vector2>();
        if(movementVect != Vector2.zero)
        {
            playerController.AddVelocity(movementVect);
        }

        // ROTATE WEAPON
        cursor.Hide();

        // We do these in order of rarity, high to low, so that, for example, a controller won't get overridden by nudging your mouse
        // If touchscreen is being used for aiming
        if (actions.gameplay.touch.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 touchPos = actions.gameplay.touch.ReadValue<Vector2>();

            // Gets the world pos pointed to by the screen 
            Vector2 worldPos = Camera.main.ScreenToViewportPoint(touchPos - new Vector2(Screen.width / 2, Screen.height / 2));
            weapon.RotateToward(worldPos);

            // Saves the cursor position for drawing later
            cursor.SetCursorPosition(touchPos);
            cursor.Show();
        }
        // If controller...
        else if (actions.gameplay.stickAim.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 stickDirection = actions.gameplay.stickAim.ReadValue<Vector2>();

            weapon.RotateToward(stickDirection);

            // Saves the cursor position for drawing later
            cursor.SetCursorDirection(stickDirection);
            cursor.Show();
        }
        // If mouse...
        else if (actions.gameplay.mouseAim.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 mousePos = actions.gameplay.mouseAim.ReadValue<Vector2>();

            // Gets the world pos pointed to by the screen 
            Vector2 worldPos = Camera.main.ScreenToViewportPoint(mousePos - new Vector2(Screen.width / 2, Screen.height / 2));
            weapon.RotateToward(worldPos);

            // Saves the cursor position for drawing later
            cursor.SetCursorPosition(mousePos);
            cursor.Show();
        }
        // Otherwise, if we're moving
        else if (actions.gameplay.move.ReadValue<Vector2>() != Vector2.zero)
        {
            weapon.RotateToward(actions.gameplay.move.ReadValue<Vector2>());
        }
    }

    private void OnEnable()
    {
        actions.gameplay.Enable();
        actions.UI.Enable();
    }

    private void OnDisable()
    {
        actions.gameplay.Disable();
        actions.UI.Disable();
    }
}
