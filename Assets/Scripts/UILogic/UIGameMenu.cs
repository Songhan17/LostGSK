using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : JuiSingleton<UIGameMenu>
{
    public override string uiPath => "MenuPanel";

    private Transform SpellCard;
    


    protected override void OnCreate()
    {
        base.OnCreate();

        SpellCard = transform.Find("SpellCard");

        Debug.Log("OnCreateOnCreateOnCreate");
        SpellCard.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("点击");
            UIGameSpellCard.Instance.Show();
        });

    }
}
