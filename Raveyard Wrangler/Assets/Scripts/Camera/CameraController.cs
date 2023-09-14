using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform drawPoint;

    Vector3 offset = new Vector3 (0, 0, -10);

    void Update()
    {
        transform.position = player.position + offset;

        // This basically just sets drawPoint's position to directly behind the player at the point where the camera starts
        drawPoint.localPosition = new Vector3(-offset.x, offset.z - offset.y, 0);
    }
}
