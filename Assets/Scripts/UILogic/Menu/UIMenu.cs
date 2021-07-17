using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    private Button start;
    private Button end;
    public GameObject lastGameObject;
    public EventSystem eventSystem;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        start = transform.Find("Start").GetComponent<Button>();
        end = transform.Find("End").GetComponent<Button>();

        start.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
        });

        end.onClick.AddListener(() =>
        {
            GameManager.Instance.EndGame();
        });

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


}
