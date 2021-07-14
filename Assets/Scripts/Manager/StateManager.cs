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
        if (this.state == GameState.Running)
        {
            InvokeRepeating("Restore", 1, 1);
        }
        else
        {
            CancelInvoke("Restore");
        }
    }

    public void Restore()
    {
        DataManager.Instance.CurrentMp += (int)DataManager.Instance.Restore;
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