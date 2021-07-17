using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadMain();
        }
    }

    public void LoadMain()
    {
        GameObjectPoolManager.Instance.Delete("Stg_01");
        GameObjectPoolManager.Instance.Delete("Shoot_1");
        ScenesManager.Instance.LoadCurrent();
    }

}
