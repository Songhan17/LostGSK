using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        StateManager.Instance.SetState(GameState.Running);

    }


    public void LoadMain()
    {
        GameObjectPoolManager.Instance.DeleteAll();
        SceneManager.LoadScene(0);
    }

}
