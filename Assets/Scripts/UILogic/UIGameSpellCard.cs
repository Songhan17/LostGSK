using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSpellCard : JuiSingleton<UIGameSpellCard>
{
    public override string uiPath => "Canvas/MenuPanel/SpellCard/Card";

    private Transform Red;
    private Transform Bule;
    private Transform Yellow;
    private Transform White;

    protected override void OnCreate()
    {
        base.OnCreate();
        Red = transform.Find("Red");
        Bule = transform.Find("Bule");
        Yellow = transform.Find("Yellow");
        White = transform.Find("White");
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

}
