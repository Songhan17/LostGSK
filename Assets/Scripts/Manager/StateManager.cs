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

        switch (this.state)
        {
            case GameState.Running:
                InvokeRepeating("Restore", 1, 1);
                Time.timeScale = 1;

                break;
            case GameState.Menu:
                CancelInvoke("Restore");
                Time.timeScale = 0;
                break;
            case GameState.Pause:
                CancelInvoke("Restore");
                Time.timeScale = 0;
                break;
            case GameState.Stop:
                CancelInvoke("Restore");
                Time.timeScale = 0;
                break;
            case GameState.InAnim:
                CancelInvoke("Restore");
                break;
            case GameState.Context:
                CancelInvoke("Restore");
                Time.timeScale = 0;
                break;
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