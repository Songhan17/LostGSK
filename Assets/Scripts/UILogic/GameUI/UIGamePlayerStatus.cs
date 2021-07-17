using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIGamePlayerStatus : JuiSubBase
{
    [JuiElement("Hp/red")]
    private Slider red = default;
    [JuiElement("Hp/hp")]
    private Text redText = default;

    [JuiElement("Mp/blue")]
    private Slider blue = default;
    [JuiElement("Mp/mp")]
    private Text blueText = default;

    protected override void OnCreate()
    {
        base.OnCreate();
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
