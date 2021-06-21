using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoSingleton<EventSystemManager>
{

    private EventSystem eventSystem;

    public GameObject lastGameObject;

    private void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void SetCurrentGameObject(GameObject go)
    {
        eventSystem.SetSelectedGameObject(go);
    }

    private void Update()
    {
        if (lastGameObject != null)
        {
            CursorLock();
        }   
    }

    // 限制鼠标点击
    public void CursorLock()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            lastGameObject = eventSystem.currentSelectedGameObject;
        }

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastGameObject);
        }

    }

    public GameObject GetCurrent()
    {
        return eventSystem.currentSelectedGameObject;
    }

}
