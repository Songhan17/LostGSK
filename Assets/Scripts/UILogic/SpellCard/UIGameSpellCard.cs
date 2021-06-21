using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSpellCard : JuiSingletonExtension<UIGameSpellCard>
{
    public override string uiPath => "MenuPanel/SpellCard/Card";

    private Transform Red;
    private Transform Blue;
    private Transform Yellow;
    private Transform White;

    protected override void OnCreate()
    {
        base.OnCreate();
        Red = transform.Find("Red");
        Blue = transform.Find("Blue");
        Yellow = transform.Find("Yellow");
        White = transform.Find("White");

        // TODO event trigger

        Red.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Red, Red);
            UIGameSpellCardList.Instance.Switch();
        });
        Blue.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Blue,Blue);
            UIGameSpellCardList.Instance.Switch();
        });
        Yellow.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Yellow,Yellow);
            UIGameSpellCardList.Instance.Switch();
        });
        White.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.White, White);
            UIGameSpellCardList.Instance.Switch();
        });

        transform.AddListener(UnityEngine.EventSystems.EventTriggerType.Cancel, _e =>
        {
            Hide();
        });

    }

    public override void Show()
    {
        base.Show();
        EventSystemManager.Instance.SetCurrentGameObject(Red.gameObject);
    }

    public override void Hide()
    {
        base.Hide();
        EventSystemManager.Instance.SetCurrentGameObject(UIGameMenu.Instance.SpellCard.gameObject);
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

    public void EquipCard(Skill skill)
    {
        SkillofButton(skill).Find("Text").GetComponent<Text>().text = skill.Name;
    }

    public void Unstall(Skill skill)
    {
        SkillofButton(skill).Find("Text").GetComponent<Text>().text = string.Empty;
    }

    private Transform SkillofButton(Skill skill)
    {
        switch (skill.Type)
        {
            case Skill.SkillType.Red: return Red;
            case Skill.SkillType.Blue: return Blue;
            case Skill.SkillType.Yellow: return Yellow;
            case Skill.SkillType.White: return White;
            default: return null;
        }
    }

    public string CurrentSkill(Skill skill)
    {
        return SkillofButton(skill).Find("Text").GetComponent<Text>().text;
    }

}
