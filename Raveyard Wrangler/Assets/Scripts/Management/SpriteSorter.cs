using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField]
    CameraController cameraController;

    GameObject[] sprites;

    void Awake()
    {
        sprites = GameObject.FindGameObjectsWithTag("Sprite");
    }

    void Update()
    {
        QuickSortSprites(0, sprites.Length - 1);

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].GetComponent<SpriteRenderer>().sortingOrder = sprites.Length - 1 - i;
        }
    }

    void QuickSortSprites(int firstIndex, int lastIndex)
    {
        if (firstIndex < lastIndex)
        {
            int splitIndex = PartitionSprites(firstIndex, lastIndex);

            QuickSortSprites(firstIndex, splitIndex - 1);
            QuickSortSprites(splitIndex + 1, lastIndex);
        }
    }

    int PartitionSprites(int firstIndex, int lastIndex)
    {
        float pivot = GetZDistanceFromCamera(sprites[firstIndex]);

        int leftIndex = firstIndex + 1;
        int rightIndex = lastIndex;

        while(true)
        {
            while(leftIndex <= rightIndex && GetZDistanceFromCamera(sprites[leftIndex]) <= pivot)
            {
                leftIndex++;
            }

            while (rightIndex >= leftIndex && GetZDistanceFromCamera(sprites[rightIndex]) >= pivot)
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

    float GetZDistanceFromCamera(GameObject gObject)
    {
        Vector3 objectPos = new Vector3(0, gObject.transform.position.y, gObject.transform.position.z);
        Vector3 cameraPos = new Vector3(0, cameraController.drawPoint.position.y, cameraController.drawPoint.position.z);
        
        return Vector3.Distance(objectPos, cameraPos);
    }
}
