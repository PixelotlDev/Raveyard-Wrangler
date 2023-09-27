using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns enemies with some interval outside of a certain radius of the player - ideally offscreen
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float spawnRate;

    [SerializeField]
    float spawnDistance;

    Transform playerTransform;
    
    float enemyTimer;

    void Awake()
    {
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
        enemyTimer += Time.deltaTime;

        while (enemyTimer > spawnRate)
        {
            float enemySpawnAngle = Random.Range(0, 360);
            Vector3 enemySpawnPos2 = Quaternion.AngleAxis(enemySpawnAngle, playerTransform.up) * playerTransform.forward;

            Instantiate(enemy, (enemySpawnPos2 * spawnDistance) + enemy.transform.position, enemy.transform.rotation);

            enemyTimer -= spawnRate;
        }
    }
}
