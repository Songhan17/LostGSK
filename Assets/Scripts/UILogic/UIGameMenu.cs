using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameMenu : JuiSingletonExtension<UIGameMenu>
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
        EventSystemManager.Instance.lastGameObject = PlayerPanel.gameObject;
    }

    public override void Hide()
    {
        base.Hide();
        EventSystemManager.Instance.lastGameObject = null;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        CancelIfInParent(transform.gameObject);
    }
}
