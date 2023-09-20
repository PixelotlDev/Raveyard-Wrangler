using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    Transform parentTransform;

    [SerializeField]
    float rotateDistance;

    void Awake()
    {
        parentTransform = GetComponentInParent<Transform>();
    }

    void Update()
    {
        
    }

    public void RotateToward(Vector3 point)
    {
        // Normalise, then adjust to map V2 x to V3 x and V2 y to V3 z
        Vector3 normalPoint = point.normalized;
        Vector3 adjustedPoint = new Vector3(normalPoint.x, 0, normalPoint.y);

        transform.localPosition = adjustedPoint * rotateDistance;

        // Calculate the angle (in 360 degrees) that we want our weapon to point in, then set our rotation to that
        float lookAngle = Vector3.SignedAngle(parentTransform.forward, adjustedPoint, parentTransform.up);
        if(lookAngle > -90 && lookAngle < 90) { lookAngle = -lookAngle; }

        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
