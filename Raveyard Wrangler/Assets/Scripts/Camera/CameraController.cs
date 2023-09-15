using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform drawPoint;

    Camera thisCamera;

    [SerializeField]
    MovementController playerController;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float cameraSize;

    [SerializeField]
    bool reduceMotion;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = player.position + offset;

        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, cameraSize * (playerController.SpeedMod - (playerController.SpeedMod - 1) / 2), 0.2f);
        
        // This basically just sets drawPoint's position to directly behind the player at the point where the camera starts
        drawPoint.localPosition = new Vector3(-offset.x, offset.z - offset.y, 0);
    }
}
