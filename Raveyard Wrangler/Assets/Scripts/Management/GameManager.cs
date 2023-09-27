using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    initialise,
    game
}

public class GameManager : MonoBehaviour
{
    // PUBLIC MANAGER
    public static GameManager instance;

    // COMMON OBJECTS
    public SettingsManager settingsManager;
    public SceneHandler sceneHandler;

    // EVENTS
    public UnityEvent ReloadSettingsEvent { get; private set; }

    // CODE VARIABLES
    GameState gameState;

    void Start()
    {
        if (GameObject.Find("Game Manager"))
        {
            Debug.LogError("GameManager already exists, but another one was initialised!");
            throw new InvalidOperationException();
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        ReloadSettingsEvent ??= new UnityEvent();

        SetState(GameStates.initialise);
    }

    void Update()
    {
        gameState.Update();
    }

    public void SetState(GameStates state)
    {
        if (gameState != null) { gameState.End(); }

        switch (state)
        {
            case GameStates.initialise:
                gameState = new InitialiseGS();
                break;
            case GameStates.game:
                gameState = new GameGS();
                break;
        }

        gameState.Start();
    }

    public void ReloadSettings()
    {
        ReloadSettingsEvent.Invoke();
    }
}
