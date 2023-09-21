using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton code from https://csharpindepth.com/Articles/Singleton

public class CommonInterface
{
    // Reference to itself that can be accessed from anywhere
    private static readonly CommonInterface instance = new CommonInterface();
    public static CommonInterface Instance
    {
        get
        {
            return instance;
        }
    }

    // INTERFACES
    public SettingsManager settingsManager;
    public SceneHandler sceneHandler;

    // EVENTS
    public UnityEvent ReloadSettingsEvent { get; private set; }

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static CommonInterface()
    {
    }

    private CommonInterface()
    {
    }

    void Awake()
    {
        settingsManager = new SettingsManager();
        sceneHandler = GameObject.Find("Scene Handler").GetComponent<SceneHandler>();

        ReloadSettingsEvent ??= new UnityEvent();
    }

    public void ReloadSettings()
    {
        ReloadSettingsEvent.Invoke();
    }
}
