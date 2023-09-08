using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class MovementController : MonoBehaviour
{
    Vector2 velocity;

    [SerializeField]
    float maxVelocity = 100;
    [SerializeField]
    float friction = 5;
    [SerializeField]
    float speedMod = 10;

    bool doFrictionX;
    bool doFrictionY;

    void Awake()
    {
        velocity = Vector2.zero;

        doFrictionX = true;
        doFrictionY = true;
    }

    void FixedUpdate()
    {
        // Apply friction so long as we aren't trying to move
        if (doFrictionX)
        {
            if (velocity.x > 0) { velocity.x -= (velocity.x < friction ? velocity.x : friction); }
            if (velocity.x < 0) { velocity.x += (-velocity.x < friction ? -velocity.x : friction); }
        }
        if (doFrictionY)
        {
            if (velocity.y > 0) { velocity.y -= (velocity.y < friction ? velocity.y : friction); }
            if (velocity.y < 0) { velocity.y += (-velocity.y < friction ? -velocity.y : friction); }
        }


        // Apply velocity cap
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
        velocity.y = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);

        // Apply velocity
        Vector3 velocityVec3 = new Vector3(velocity.x, 0, velocity.y);
        transform.position += (velocityVec3 / 10) * Time.fixedDeltaTime;

        // Cleanup
        doFrictionX = true;
        doFrictionY = true;
    }

    // Move by some set amount in a normalised direction
    public void Move(Vector2 direction)
    {
        // If our force is opposite our current velocity, apply friction. Otherwise, do not.
        doFrictionX = (direction.x <= 0 && velocity.x > 0) || (direction.x >= 0 && velocity.x < 0);
        doFrictionY = (direction.y <= 0 && velocity.y > 0) || (direction.y >= 0 && velocity.y < 0);

        velocity += direction.normalized * speedMod;
    }
}
