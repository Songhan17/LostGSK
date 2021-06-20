using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSpellCard : JuiSingleton<UIGameSpellCard>
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


    public void EquipCard(Skill skill)
    {
        switch (skill.Type)
        {
            case Skill.SkillType.Red: 
                Red.Find("Text").GetComponent<Text>().text = skill.Name;
                break;
            case Skill.SkillType.Blue:
                Blue.Find("Text").GetComponent<Text>().text = skill.Name;
                break;
            case Skill.SkillType.Yellow:
                Yellow.Find("Text").GetComponent<Text>().text = skill.Name;
                break;
            case Skill.SkillType.White:
                White.Find("Text").GetComponent<Text>().text = skill.Name;
                break;
            default:
                break;
        }
    }

}
