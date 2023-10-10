using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls enemy behaviour
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float minDistanceFromPlayer;

    [SerializeField]
    float fireRate;

    MovementController movementController;

    Transform playerTransform;

    float shotTimer;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (gObject.layer == LayerMask.NameToLayer("Entity"))
            {
                playerTransform = gObject.transform;
                break;
            }
        }
    }

    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        Quaternion rotate45 = Quaternion.Euler(45, 0, 0);

        Vector3 rotatedDTP = rotate45 * directionToPlayer;
        rotatedDTP = new Vector3(rotatedDTP.x, rotatedDTP.z, 0);

        if (directionToPlayer.magnitude > minDistanceFromPlayer)
        {
            movementController.AddVelocity(rotatedDTP);
        }
        else
        {
            movementController.AddVelocity(-rotatedDTP);
        }

        shotTimer += Time.deltaTime;
        while (shotTimer > fireRate)
        {
            FireBullet();
            shotTimer = 0;
        }
    }

    /// <summary>
    /// Creates a bullet, then fires it at the player
    /// </summary>
    public void FireBullet()
    {
        Vector3 position = transform.position;
        GameObject newBullet = Instantiate(bullet, position, bullet.transform.rotation);

        Vector3 direction = playerTransform.position - transform.position;
        Vector3 adjustedDirection = new Vector2(direction.x, direction.z);
        newBullet.GetComponent<MovementController>().AddVelocity(adjustedDirection);
    }
}
