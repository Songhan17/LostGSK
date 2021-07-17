using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[JuiPanel(Name = "MenuPanel", EnableUpdate = true, IsPreBind = true)]
public class UIGameMenu : JuiBase<UIGameMenu>
{

    public Transform PlayerPanel { get; private set; }
    public Transform SpellCard { get; private set; }
    public Transform Title { get; private set; }

    [JuiElementSubPanel(Path = "PlayerPanel/property", EnableUpdate = true)]
    private UIPlayerPanelProperty property = default;
    public UIPlayerPanelProperty UIPlayerPanelProperty { get => property; }

    [JuiElementSubPanel(Path = "SpellCard/Card", EnableUpdate = true)]
    private UIGameSpellCard Card = default;
    public UIGameSpellCard UIGameSpellCard { get => Card; }

    [JuiElementSubPanel(Path = "SpellCard/CardList", EnableUpdate = true)]
    private UIGameSpellCardList CardList = default;
    public UIGameSpellCardList UIGameSpellCardList { get => CardList; }

    protected override void OnCreate()
    {
        base.OnCreate();

        PlayerPanel = transform.Find("PlayerPanel");
        SpellCard = transform.Find("SpellCard");
        Title = transform.Find("Title");

        PlayerPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            property.Switch();
        });

        SpellCard.GetComponent<Button>().onClick.AddListener(() =>
        {
            Card.Show();
        });

        Title.GetComponent<Button>().onClick.AddListener(() =>
        {
            ScenesManager.Instance.Menu();
        });
    }

    protected override void OnShow()
    {
        base.OnShow();
        StateManager.Instance.SetState(GameState.Menu);
        EventSystemManager.Instance.SetCurrentGameObject(PlayerPanel.gameObject);
        EventSystemManager.Instance.lastGameObject = PlayerPanel.gameObject;
        PlayerController.Instance.enabled = false;
    }

    protected override void OnHide()
    {
        base.OnHide();
        StateManager.Instance.SetState(GameState.Running);
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
