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
        // If mouse is being used for aiming
        if(actions.gameplay.mouseAim.ReadValue<Vector2>() != Vector2.zero)
        {
            // Gets the world pos pointed to by the screen 
            Vector2 worldPos = Camera.main.ScreenToViewportPoint(actions.gameplay.mouseAim.ReadValue<Vector2>() - new Vector2(Screen.width / 2, Screen.height / 2));
            weapon.RotateToward(worldPos);
        }
        else if(actions.gameplay.stickAim.ReadValue<Vector2>() != Vector2.zero)
        {
            weapon.RotateToward(actions.gameplay.stickAim.ReadValue<Vector2>());
        }
        else if(actions.gameplay.move.ReadValue<Vector2>() != Vector2.zero)
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
