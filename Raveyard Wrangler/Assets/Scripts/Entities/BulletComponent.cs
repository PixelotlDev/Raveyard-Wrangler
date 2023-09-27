using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float damage;

    [HideInInspector]
    public bool hasHitSomething = false;

    [SerializeField]
    float lifetime;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
