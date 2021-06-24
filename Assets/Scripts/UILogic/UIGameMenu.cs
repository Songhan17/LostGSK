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

        PlayerPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIPlayerPanelProperty.Instance.Switch();
        });

        SpellCard.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCard.Instance.Show();
        });
    }

    protected override void OnShow()
    {
        base.OnShow();
        EventSystemManager.Instance.SetCurrentGameObject(PlayerPanel.gameObject);
        EventSystemManager.Instance.lastGameObject = PlayerPanel.gameObject;
    }

    protected override void OnHide()
    {
        base.OnHide();
        EventSystemManager.Instance.lastGameObject = null;
        SkillController.Instance.Save();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsFocus)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Hide();
            }
        }
    }
}
