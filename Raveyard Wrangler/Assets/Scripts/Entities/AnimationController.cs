using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    PlayerActions actions;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer sprite;

    int directionFacing;

    bool flipped;

    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActions();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVect = actions.gameplay.move.ReadValue<Vector2>();

        // Set the 'Facing' parameter
        if (movementVect.x > 0)
        {
            directionFacing = 1; // right
            flipped = movementVect.x < 0;
        }
        else if (movementVect.x < 0)
        {
            directionFacing = 2; // left
            flipped = movementVect.x < 0;
        }
        else if (movementVect.y < 0)
        {
            directionFacing = 0; // up
            flipped = false;
        }
        else if (movementVect.y > 0)
        {
            directionFacing = 3; // down
            flipped = false;
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
