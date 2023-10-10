using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// List of game scenes
/// </summary>
public enum GameScene
{
    Setup,
    Level
}

/// <summary>
/// Parent class for all game states
/// <para>
/// Should use a list of transitions instead of an "end" update (and perhaps ditch the start update too)
/// </para>
/// </summary>
public abstract class GameState
{
    // GAME MANAGER
    protected GameManager gameManager = GameManager.instance;

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void End() { }
}

public class InitialiseGS : GameState
{
    public override void Start()
    {
        SceneManager.LoadScene(GameScene.Level.ToString());
    }
}

public class GameGS : GameState
{
    
}