using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[JuiPanel(UiPath = "MenuPanel",EnableUpdate =true)]
public class UIGameMenu : JuiSingletonExtension<UIGameMenu>
{

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
        Time.timeScale = 0;
        EventSystemManager.Instance.SetCurrentGameObject(PlayerPanel.gameObject);
        EventSystemManager.Instance.lastGameObject = PlayerPanel.gameObject;
        PlayerController.Instance.enabled = false;
    }

    protected override void OnHide()
    {
        base.OnHide();
        Time.timeScale = 1;
        EventSystemManager.Instance.lastGameObject = null;
        SkillController.Instance.Save();
        PlayerController.Instance.enabled = true;
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
