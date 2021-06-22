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
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Red, Red, GetCurrentName(Red));
            UIGameSpellCardList.Instance.Switch();
        });
        Blue.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Blue,Blue, GetCurrentName(Blue));
            UIGameSpellCardList.Instance.Switch();
        });
        Yellow.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.Yellow,Yellow, GetCurrentName(Yellow));
            UIGameSpellCardList.Instance.Switch();
        });
        White.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIGameSpellCardList.Instance.Refresh(Skill.SkillType.White, White, GetCurrentName(White));
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

    // 更新符卡名
    public void EquipCard(Skill skill)
    {
        SkillofButton(skill).Find("Text").GetComponent<Text>().text = skill.Name;
    }
    // 卸载符卡
    public void Unstall(Skill skill)
    {
        SkillofButton(skill).Find("Text").GetComponent<Text>().text = string.Empty;
    }

    // 根据技能返回组件类型
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

    // 根据技能获取当前已装备技能名
    public string CurrentSkill(Skill skill)
    {
        return SkillofButton(skill).Find("Text").GetComponent<Text>().text;
    }

    // 根据组件获取text
    public string GetCurrentName(Transform transform)
    {
        return transform.Find("Text").GetComponent<Text>().text;
    }
}
