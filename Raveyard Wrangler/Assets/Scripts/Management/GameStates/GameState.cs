using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameManager.sceneHandler.LoadScene(Scene.Level);
    }
}

public class GameGS : GameState
{
    
}