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

        SpellCard.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCard.Instance.Switch();
            //if (!UIGameSpellCard.Instance.IsShow)
            //{
            //    UIGameSpellCardList.Instance.Hide();
            //}
        });

    }
}
