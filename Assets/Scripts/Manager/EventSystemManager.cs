using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoSingleton<EventSystemManager>
{

    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void SetCurrentGameObject(GameObject go)
    {
        eventSystem.SetSelectedGameObject(go);
    }

}
