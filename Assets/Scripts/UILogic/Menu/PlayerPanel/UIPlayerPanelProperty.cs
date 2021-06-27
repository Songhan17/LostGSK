using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerPanelProperty : JuiSingletonExtension<UIPlayerPanelProperty>
{
    public override string uiPath => "MenuPanel/PlayerPanel/property";

    private int maxItemCount;



    protected override void OnCreate()
    {
        base.OnCreate();
        maxItemCount = transform.childCount;
    }

    protected override void OnShow()
    {
        base.OnShow();
        Refresh(DataManager.Instance.ToString());
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
