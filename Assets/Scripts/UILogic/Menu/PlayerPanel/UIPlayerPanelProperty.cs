using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerPanelProperty : JuiSubBase
{

    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnShow()
    {
        base.OnShow();
        Refresh(DataManager.Instance.ToString());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        EventSystemManager.Instance.SetCurrentGameObject(transform.parent.gameObject);
        if (IsFocus)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Hide();
            }
        }
    }

    public void Refresh(string target)
    {
        string[] data = target.Split(';');

        for (int i = 0; i < data.Length; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = data[i];
        }
    }

}
