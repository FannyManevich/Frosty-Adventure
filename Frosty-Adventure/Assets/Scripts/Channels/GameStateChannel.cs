using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerController;

[CreateAssetMenu(fileName = "GameState Channel", menuName = "Channels/GameState Channel", order = 1)]
public class GameStateChannel : ScriptableObject
{
    public event Action<GameState> StateEnter;
    public event Action<GameState> StateExit;
    //public event Action<GameState> GetCurrentState;

    public void StateEntered(GameState state)
    {
        StateEnter?.Invoke(state);
    }

    public void StateExited(GameState state)
    {
        StateExit?.Invoke(state);
    }

}