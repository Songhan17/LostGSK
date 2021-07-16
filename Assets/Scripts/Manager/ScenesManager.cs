using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoSingleton<ScenesManager>
{
    private List<string> currentStage = new List<string>();

    IEnumerator Start()
    {
        currentStage?.Clear();
        currentStage.Add("Stage1-1");
        currentStage.Add("Stage1-2");
        DataManager.Instance.InitData();

        foreach (GameObject rootObj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            DontDestroyOnLoad(rootObj);
        }
        yield return new WaitForSeconds(1);
        GameManager.Instance.LoadMain();
    }

    public void LoadCurrent()
    {
        Invoke("Load", 2);

    }

    public void Load()
    {
        Debug.Log("Start");
        Debug.Log(GameObject.Find("__System").name);
        PlayerController.Instance.transform.position = GameObject.Find("Position").transform.GetChild(0).position;
        StageController.Instance.LoadStage(currentStage);
    }

}
