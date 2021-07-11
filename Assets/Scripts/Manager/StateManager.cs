using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoSingleton<StateManager>
{
    protected GameState state;

    public GameState GetState()
    {
        return state;
    }

    public void SetState(GameState state)
    {
        this.state = state;
    }

}
public enum GameState
{
    Running,
    Menu,
    Pause,
    Stop,
    InAnim,
    Context
}