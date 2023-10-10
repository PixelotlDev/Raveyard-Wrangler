using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField]
    CameraController cameraController;

    GameObject[] sprites;

    void Update()
    {
        sprites = GameObject.FindGameObjectsWithTag("Sprite");

        QuickSortSprites(0, sprites.Length - 1);

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].GetComponent<SpriteRenderer>().sortingOrder = sprites.Length - 1 - i;
        }
    }

    /// <summary>
    /// Sorts an array by distance from the camera, only works on GameObject arrays
    /// </summary>
    /// <param name="firstIndex">Start of the array of sprites being sorted</param>
    /// <param name="lastIndex">End of the array of sprites being sorted</param>
    void QuickSortSprites(int firstIndex, int lastIndex)
    {
        if (firstIndex < lastIndex)
        {
            int splitIndex = PartitionSprites(firstIndex, lastIndex);

            QuickSortSprites(firstIndex, splitIndex - 1);
            QuickSortSprites(splitIndex + 1, lastIndex);
        }
    }

    /// <summary>
    /// Recursive implementation of QuickSort
    /// </summary>
    /// <param name="firstIndex">Start of the portion of sprites being sorted</param>
    /// <param name="lastIndex">End of the portion of sprites being sorted</param>
    /// <returns>Point at which the portion was split</returns>
    private int PartitionSprites(int firstIndex, int lastIndex)
    {
        float pivot = GetDistanceFromCamera(sprites[firstIndex]);

        int leftIndex = firstIndex + 1;
        int rightIndex = lastIndex;

        while(true)
        {
            while(leftIndex <= rightIndex && GetDistanceFromCamera(sprites[leftIndex]) <= pivot)
            {
                leftIndex++;
            }

            while (rightIndex >= leftIndex && GetDistanceFromCamera(sprites[rightIndex]) >= pivot)
            {
                rightIndex--;
            }

            if(rightIndex < leftIndex) { break; }

            // Here we use two tuples created on the fly to swap two indexes in an array. Who knew that was a thing?
            (sprites[rightIndex], sprites[leftIndex]) = (sprites[leftIndex], sprites[rightIndex]);
        }

        (sprites[rightIndex], sprites[firstIndex]) = (sprites[firstIndex], sprites[rightIndex]);

        return rightIndex;
    }

    /// <summary>
    /// Finds the distance between an object and the camera, ignoring the x axis
    /// </summary>
    /// <param name="gObject">Target object</param>
    /// <returns>Distance of object from camera</returns>
    float GetDistanceFromCamera(GameObject gObject)
    {
        Vector3 objectPos = new Vector3(0, gObject.transform.position.y, gObject.transform.position.z);
        Vector3 cameraPos = new Vector3(0, cameraController.drawPoint.position.y, cameraController.drawPoint.position.z);
        
        return Vector3.Distance(objectPos, cameraPos);
    }
}
