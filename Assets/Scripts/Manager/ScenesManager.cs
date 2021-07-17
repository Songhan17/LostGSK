using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoSingleton<ScenesManager>
{
    private List<string> currentStage = new List<string>();

    void Start()
    {
        currentStage?.Clear();
        currentStage.Add("Stage1-1");
        currentStage.Add("Stage1-2");
        DataManager.Instance.InitData();

        foreach (GameObject rootObj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            DontDestroyOnLoad(rootObj);
        }
        GameManager.Instance.LoadMain();
    }

    public void LoadCurrent()
    {
        StartCoroutine(Load());

    }

    IEnumerator Load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;

                yield return null;
            }
        }
        PlayerController.Instance.transform.position = GameObject.Find("Position").transform.GetChild(0).position;
        DataManager.Instance.InitData();
        StageController.Instance.LoadStage(currentStage);
    }

}
