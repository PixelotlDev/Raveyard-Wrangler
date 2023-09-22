using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Setup,
    Level
}

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
