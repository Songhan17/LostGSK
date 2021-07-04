using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[JuiPanel(UiPath = "UI/Status/Hp",EnableUpdate =true,IsPreBind =false)]
public class UIGamePlayerStatus : JuiSingletonExtension<UIGamePlayerStatus>
{
    private Slider red;
    private Text text;

    protected override void OnCreate()
    {
        base.OnCreate();
        red = transform.Find("red").GetComponent<Slider>();
        text = transform.Find("hp").GetComponent<Text>();
        SetMax();
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        red.value = DataManager.Instance.CurrentHp;
        text.text = red.value.ToString();
    }

    public void SetMax()
    {
        red.maxValue = DataManager.Instance.Hp;
    }

}
