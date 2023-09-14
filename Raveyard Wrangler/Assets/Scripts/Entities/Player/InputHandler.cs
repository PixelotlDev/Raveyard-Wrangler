using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerActions actions;

    [SerializeField]
    MovementController playerController;

    void Awake()
    {
        actions = new PlayerActions();
    }

    void Update()
    {
        Vector2 movementVect = actions.gameplay.move.ReadValue<Vector2>();
        if (movementVect != Vector2.zero)
        {
            playerController.AddVelocity(movementVect);
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
