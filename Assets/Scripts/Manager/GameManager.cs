using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
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
        GameObjectPoolManager.Instance.DeleteAll();
        SceneManager.LoadScene(0);
    }

}
