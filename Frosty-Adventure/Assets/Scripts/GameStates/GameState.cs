using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState", order = 1)]
public class GameState : ScriptableObject
{
    public enum State
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public State currentState;
    public event Action<State> OnStateChanged; 

    public string stateName
    {
        get
        {
            return currentState.ToString();
        }
    }

    public void SetState(State newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            OnStateChanged?.Invoke(currentState);
        }
    }
}