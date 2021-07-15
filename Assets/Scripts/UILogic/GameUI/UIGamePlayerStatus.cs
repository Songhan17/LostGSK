using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[JuiPanel(UiPath = "UI/Status",EnableUpdate =true,IsPreBind =true)]
public class UIGamePlayerStatus : JuiSingletonExtension<UIGamePlayerStatus>
{
    private Slider red,blue;
    private Text redText,blueText;

    protected override void OnCreate()
    {
        base.OnCreate();
        red = transform.Find("Hp/red").GetComponent<Slider>();
        redText = transform.Find("Hp/hp").GetComponent<Text>();
        blue = transform.Find("Mp/blue").GetComponent<Slider>();
        blueText = transform.Find("Mp/mp").GetComponent<Text>();
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
        redText.text = red.value.ToString();
        blue.value = DataManager.Instance.CurrentMp;
        blueText.text = blue.value.ToString();
    }

    public void SetMax()
    {
        red.maxValue = DataManager.Instance.Hp;
        blue.maxValue = DataManager.Instance.Mp;
    }

    
}
