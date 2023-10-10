using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// List of possible game states
/// </summary>
public enum GameStates
{
    initialise,
    game
}

/// <summary>
/// Overall game manager - serves as an interface from which to grab references to globally used classes, and a way to control which game state is currently active.
/// <para>
/// Somewhat poorly designed, a manager such as this should have some kind of dictionary of important objects.
/// More importantly, states should be able to have well-defined transitions between them, rather than generic start, running, and end states.
/// </para>
/// </summary>
public class GameManager : MonoBehaviour
{
    // PUBLIC MANAGER
    public static GameManager instance;

    // COMMON OBJECTS
    public SettingsManager settingsManager;

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

    /// <summary>
    /// Sets the game's state, and runs the current state's end function, then the next state's start function.
    /// </summary>
    /// <param name="state">Game state to transition into.</param>
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

    /// <summary>
    /// Calls every function subscribed to the "Reload Settings" event.
    /// </summary>
    public void ReloadSettings()
    {
        ReloadSettingsEvent.Invoke();
    }
}
