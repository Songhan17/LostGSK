using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    private CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();
    }

    public Vector2 ChangeStage(int pwd)
    {
        if (pwd == 2)
        {
            confiner.m_BoundingShape2D = GameObject.Find("Stage1-2/mapCollider").GetComponent<PolygonCollider2D>();
            return GameObject.Find("Stage1-2/door1-1").transform.position;
        }
        else
        {
            confiner.m_BoundingShape2D = GameObject.Find("Stage1-1/mapCollider").GetComponent<PolygonCollider2D>();
            return GameObject.Find("Stage1-1/door1-2").transform.position;
        }
    }

}
