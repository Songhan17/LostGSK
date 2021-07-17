using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void StartGame()
    {
        ScenesManager.Instance.InitScene();
        //LoadMain();
    }

    public void EndGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
