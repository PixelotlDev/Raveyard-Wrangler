using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
