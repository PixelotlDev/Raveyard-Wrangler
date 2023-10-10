using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Feeds the animator component values based on the player's current behaviour
/// </summary>
public class AnimationController : MonoBehaviour
{
    PlayerActions actions;

    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer sprite;

    int directionFacing;

    void Awake()
    {
        actions = new PlayerActions();
    }

    void Update()
    {
        Vector2 movementVect = actions.gameplay.move.ReadValue<Vector2>();

        // Set the 'Facing' parameter
        if (movementVect.x > 0)
        {
            directionFacing = 1; // right
        }
        else if (movementVect.x < 0)
        {
            directionFacing = 2; // left
        }
        else if (movementVect.y < 0)
        {
            directionFacing = 0; // up
        }
        else if (movementVect.y > 0)
        {
            directionFacing = 3; // down
        }

        animator.SetInteger("Facing", directionFacing);

        // Set the 'Walking' parameter
        if (movementVect != Vector2.zero)
        {
            animator.SetBool("Walking", true);
        }
        else { animator.SetBool("Walking", false); }
    }

    private void OnEnable()
    {
        actions.gameplay.Enable();
    }

    private void OnDisable()
    {
        actions.gameplay.Disable();
    }
}
