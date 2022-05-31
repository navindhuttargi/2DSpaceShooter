using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController 
{
    public enum GameStates
    {
        initialize = 0,
        start,
        running,
        end
    }
    private Dictionary<GameStates, BaseState> gameState;
    private BaseState currentState;
    private BaseState previousState;
    private GameStates currentStateType;
    private GameStates previousStateType;
    public StateController(GameManager gameManager,GameStates gameStates)
    {
        gameState = new Dictionary<GameStates, BaseState>();
        gameState.Add(GameStates.initialize, new InitializeState(gameManager,this));
        gameState.Add(GameStates.start, new StartState(gameManager,this));
        gameState.Add(GameStates.running, new RunningState(gameManager,this));
        gameState.Add(GameStates.end, new EndState(gameManager,this));
        InitializeGameState(gameStates);
    }
    public void InitializeGameState(GameStates initStateType)
    {
        BaseState initState;

        if (gameState.TryGetValue(initStateType, out initState))
        {
            currentState = initState;

            Debug.Log("Entry");
            currentState.Entry();
        }
        else
        {
            Debug.LogError(initStateType + " is not a valid board state type");
        }
    }

    public void ChangeState(GameStates newStateType)
    {
        BaseState newState;
        if (currentStateType == newStateType)
        {
            return;
        }
        if (gameState.TryGetValue(newStateType, out newState))
        {
            if (currentState != null)
            {
                currentState.Exit();

            }
            previousState = currentState;
            previousStateType = currentStateType;
            currentState = newState;
            currentStateType = newStateType;
            currentState.Entry();
        }
        else
        {
            Debug.LogError(newStateType + " is not a valid board state type");
        }
    }
    public void StateUpdate()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
    }
}
