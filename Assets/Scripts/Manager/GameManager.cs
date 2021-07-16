using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        SkillManager.Instance.ParseSkillJson();
        EnemyManager.Instance.ParseEnemyJson();
    }
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        StateManager.Instance.SetState(GameState.Running);

    }


    public void LoadMain()
    {
        GameObjectPoolManager.Instance.DeleteAll();
        StateManager.Instance.SetState(GameState.Stop);
        SceneManager.LoadScene(2);
        StateManager.Instance.SetState(GameState.Running);
        ScenesManager.Instance.LoadCurrent();

    }

}
