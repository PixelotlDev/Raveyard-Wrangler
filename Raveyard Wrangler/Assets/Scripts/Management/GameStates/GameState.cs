using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    Setup,
    Level
}

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