using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public delegate void EventAction();
    public event EventAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}
