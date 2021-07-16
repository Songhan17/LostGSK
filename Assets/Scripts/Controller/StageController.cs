using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    private CinemachineConfiner confiner;
    private CinemachineVirtualCamera virtualCamera;
    private Transform StageParent;

    private Dictionary<string, Stage> CurrentScenes = new Dictionary<string, Stage>();

    private void Awake()
    {
        confiner = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeStage(string door,string origin)
    {
        confiner.m_BoundingShape2D = CurrentScenes[door].mapCollider;

        StageParent.Find(door).gameObject.SetActive(true);
        foreach (Transform item in StageParent.Find(door + "/enemies").transform)
        {
            item.gameObject.SetActive(true);
        }
        
        StageParent.Find(origin).gameObject.SetActive(false);
    }

    public void LoadStage(List<string> stage)
    {
        CurrentScenes?.Clear();
        StageParent = GameObject.Find("Stage").transform;
        stage.ForEach(e =>
        {
            Stage stages = new Stage();
            Transform parent = StageParent.Find(e);
            stages.name = e;
            stages.mapCollider = parent.Find("mapCollider").GetComponent<PolygonCollider2D>();
            foreach (Transform item in parent.Find("door").transform)
            {
                stages.door.Add(item);
            }
            foreach (Transform item in parent.Find("enemies").transform)
            {
                stages.enemy.Add(item);
            }
            CurrentScenes.Add(e, stages);
        });
        
        confiner.m_BoundingShape2D = CurrentScenes[stage[0]].mapCollider;
    }

    public Stage GetCurrentStage()
    {
        Debug.Log(CurrentScenes.Count);
        foreach (Stage item in CurrentScenes.Values.ToList())
        {
            Debug.Log(confiner.m_BoundingShape2D == item.mapCollider);
            if (confiner.m_BoundingShape2D == item.mapCollider)
            {
                return CurrentScenes[item.name];
            }
        }
        return null;
    }

    public void FocusView(Transform target)
    {
        virtualCamera.m_Follow = target;
        virtualCamera.m_Lens.OrthographicSize = 2;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield  return new WaitForSeconds(1f);
        virtualCamera.m_Follow = PlayerController.Instance.transform;
        virtualCamera.m_Lens.OrthographicSize = 3.5f;
    }

}
