using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{
    // UNITY CLASSES
    [SerializeField]
    BoxCollider bCollider;

    // VECTORS
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

    // FLOATS
    [SerializeField]
    float maxVelocity;
    [SerializeField]
    float baseFriction;
    [SerializeField]
    float velocityMod;
    [SerializeField]
    float _speedMod;
    public float SpeedMod
    {
        get
        {
            return _speedMod;
        }
        private set
        {
            _speedMod = value;
        }
    }

    // BOOLS
    [SerializeField]
    bool doCollision = true;

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
        // We multiply this by 10 to make the numbers look nicer in the editor. Probably not best practice
        float friction = (baseFriction * 10) * Time.deltaTime;

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
        Vector3 VelocityVec3 = new Vector3(_velocity.x, _velocity.y, _velocity.y) * _speedMod / 50;

        if (VelocityVec3 != Vector3.zero)
        {
            // We divide VelocityVec3 by 50 to make the numbers look nicer in the editor. Probably not best practice
            if (doCollision) { MoveWithCollision(VelocityVec3); }
            else { transform.position += VelocityVec3; }
        }

        // Cleanup
        doFrictionX = true;
        doFrictionY = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 movement = new Vector3(_velocity.x, _velocity.y, _velocity.y) * _speedMod / 50;

        Gizmos.DrawWireCube(transform.position + (transform.rotation * bCollider.center) + movement, bCollider.size);
    }

    // Move by some set amount in a normalised direction
    public void AddVelocity(Vector2 direction)
    {
        // If our force is opposite our current Velocity, apply Friction. Otherwise, do not.
        doFrictionX = (direction.x <= 0 && _velocity.x > 0) || (direction.x >= 0 && _velocity.x < 0);
        doFrictionY = (direction.y <= 0 && _velocity.y > 0) || (direction.y >= 0 && _velocity.y < 0);

        // We multiply speedMod by 10 to make the numbers look nicer in the editor. Probably not best practice
        _velocity += direction.normalized * (velocityMod * 10) * Time.deltaTime;
    }

    void MoveWithCollision(Vector3 movement)
    {
        if (Physics.BoxCast(transform.position + (transform.rotation * bCollider.center) - movement.normalized * 0.01f, bCollider.size / 2,
            movement.normalized, out RaycastHit hit, transform.rotation, movement.magnitude, LayerMask.GetMask("Obstacle")))
        {
            Vector3 correctedNormal = Quaternion.Inverse(hit.transform.rotation) * hit.normal;
            // TODO: fix forward and backward offset
            Vector3 boxOffset = new((correctedNormal == Vector3.left ? -bCollider.size.x / 2 : 0) + (correctedNormal == Vector3.right ? bCollider.size.x / 2 : 0),
                                    (correctedNormal == Vector3.forward ? bCollider.size.z / 2 : 0) + (correctedNormal == Vector3.back ? -bCollider.size.z / 2 : 0),
                                    (correctedNormal == Vector3.forward ? bCollider.size.z / 2 : 0) + (correctedNormal == Vector3.back ? -bCollider.size.z / 2 : 0));

            transform.position += ((hit.distance * movement.normalized) + boxOffset);
            _velocity = Vector2.zero;
        }
        else
        {
            transform.position += movement;
        }
    }
}
