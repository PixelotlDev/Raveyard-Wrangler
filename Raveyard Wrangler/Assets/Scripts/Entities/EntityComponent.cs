using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information and behaviour for entities. Part of a rushed component system made near the end of development.
/// </summary>
public class EntityComponent : MonoBehaviour
{
    // GAME MANAGER
    GameManager gameManager = GameManager.instance;

    // EDITOR VARIABLES
    [SerializeField]
    float health;

    void Update()
    {
        if (health <= 0)
        {
            if (CompareTag("Player"))
            {
                gameManager.SetState(GameStates.initialise);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        BulletComponent bullet = other.GetComponent<BulletComponent>();
        if(bullet != null && bullet.gameObject.tag != tag && bullet.hasHitSomething == false)
        {
            health -= bullet.damage;

            bullet.hasHitSomething = true;
            Destroy(other.gameObject);
        }
    }
}
