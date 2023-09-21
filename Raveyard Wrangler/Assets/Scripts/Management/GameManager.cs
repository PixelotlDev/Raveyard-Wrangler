using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    initialise,
    title,
    game
}

public class GameManager : MonoBehaviour
{
    // CODE VARIABLES
    GameState gameState;

    void Awake()
    {
        gameState = GameState.initialise;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.initialise:
                InitialiseUpdate();
                break;

            case GameState.title:
                TitleUpdate();
                break;

            case GameState.game:
                GameUpdate();
                break;

            default:
                break;
        }
    }

    public void SetState(GameState state)
    {
        gameState = state;

        switch (gameState)
        {
            case GameState.initialise:
                InitialiseStart();
                break;

            case GameState.title:
                TitleStart();
                break;

            case GameState.game:
                GameStart();
                break;

            default:
                break;
        }
    }
    void InitialiseStart()
    {
        CommonInterface.Instance.sceneHandler.NextScene();
    }

    void TitleStart()
    {

    }

    void GameStart()
    {

    }

    void InitialiseUpdate()
    {

    }

    void TitleUpdate()
    {

    }

    void GameUpdate()
    {

    }
}
