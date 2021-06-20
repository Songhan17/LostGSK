using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : JuiSingleton<UIGameMenu>
{
    public override string uiPath => "MenuPanel";

    public Transform PlayerPanel { get; private set; }
    public Transform SpellCard { get; private set; }



    protected override void OnCreate()
    {
        base.OnCreate();

        PlayerPanel = transform.Find("PlayerPanel");
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

    protected override void OnShow()
    {
        base.OnShow();
        EventSystemManager.Instance.SetCurrentGameObject(PlayerPanel.gameObject);
    }

    public override void Hide()
    {
        base.Hide();

    }

}
