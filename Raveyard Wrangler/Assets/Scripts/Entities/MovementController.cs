using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    Vector2 _velocity;
    public Vector2 Velocity
    { 
        get 
        {
            return _velocity;
        }
        private set
        {
            _velocity = value;
        }
    }

    [SerializeField]
    float maxVelocity = 80;
    [SerializeField]
    float friction = 7.5f;
    [SerializeField]
    float speedMod = 4;

    bool doFrictionX;
    bool doFrictionY;

    void Awake()
    {
        _velocity = Vector2.zero;

        doFrictionX = true;
        doFrictionY = true;
    }

    void FixedUpdate()
    {
        // Apply Friction so long as we aren't trying to move
        if (doFrictionX)
        {
            if (_velocity.x > 0) { _velocity.x -= (_velocity.x < friction ? _velocity.x : friction); }
            if (_velocity.x < 0) { _velocity.x += (-_velocity.x < friction ? -_velocity.x : friction); }
        }
        if (doFrictionY)
        {
            if (_velocity.y > 0) { _velocity.y -= (_velocity.y < friction ? _velocity.y : friction); }
            if (_velocity.y < 0) { _velocity.y += (-_velocity.y < friction ? -_velocity.y : friction); }
        }


        // Apply Velocity cap
        _velocity.x = Mathf.Clamp(_velocity.x, -maxVelocity, maxVelocity);
        _velocity.y = Mathf.Clamp(_velocity.y, -maxVelocity, maxVelocity);

        // Apply Velocity
        // We apply _velocity.y vertically as well so that our movement lines up with the weird layout we have going on
        Vector3 VelocityVec3 = new Vector3(_velocity.x, _velocity.y, _velocity.y);
        transform.position += (VelocityVec3 / 10) * Time.fixedDeltaTime;


        // Cleanup
        doFrictionX = true;
        doFrictionY = true;
    }

    // Move by some set amount in a normalised direction
    public void AddVelocity(Vector2 direction)
    {
        // If our force is opposite our current Velocity, apply Friction. Otherwise, do not.
        doFrictionX = (direction.x <= 0 && _velocity.x > 0) || (direction.x >= 0 && _velocity.x < 0);
        doFrictionY = (direction.y <= 0 && _velocity.y > 0) || (direction.y >= 0 && _velocity.y < 0);

        _velocity += direction.normalized * speedMod;
    }
}
