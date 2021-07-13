using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoSingleton<ScenesManager>
{
    private List<string> currentStage = new List<string>();

    private void Start()
    {
        currentStage.Add("Stage1-1");
        currentStage.Add("Stage1-2");
        StageController.Instance.LoadStage(currentStage);
    }

}
