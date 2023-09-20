using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject grassPrefab;

    [SerializeField]
    Transform terrainParent;


    [SerializeField]
    Vector2 levelSize;

    void Awake()
    {
        // We need to offset this to centre the level grid to (0, 0)
        Vector2 halfLevelSize = (levelSize - Vector2.one) / 2;

        // Here we spawn the grass
        for (int y = 0; y < levelSize.y; y++)
        {
            for (int x = 0; x < levelSize.x; x++)
            {
                Vector3 tilePos = new((x - halfLevelSize.x) * grassPrefab.transform.localScale.x,
                                      (y - halfLevelSize.y) * grassPrefab.transform.localScale.x,
                                      (y - halfLevelSize.y) * grassPrefab.transform.localScale.x);
                Instantiate(grassPrefab, tilePos, grassPrefab.transform.rotation, terrainParent);
            }
        }
    }
}