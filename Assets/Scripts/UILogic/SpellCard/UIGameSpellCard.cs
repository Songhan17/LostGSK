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
    public List<Skill> skills;

    protected override void OnCreate()
    {
        base.OnCreate();
        Red = transform.Find("Red");
        Blue = transform.Find("Blue");
        Yellow = transform.Find("Yellow");
        White = transform.Find("White");

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponentInChildren<Text>().text = string.Empty;
        }

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

        InitUI();

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
        SkillController.Instance.UpdateSkillDict(skills);
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

    // 更新UI
    public void Refresh(Skill skill, bool equip)
    {
        if (equip)
        {
            SkillofButton(skill).Find("Text").GetComponent<Text>().text = skill.Name;
        }
        else
        {
            SkillofButton(skill).Find("Text").GetComponent<Text>().text = string.Empty;
        }
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

    // 初始化加载
    public void InitUI()
    {
       List<Skill> skills = SkillController.Instance.GetSkills();
        skills?.ForEach(s =>
        {
            if (s.IsEquip)
            {
                Refresh(s, true);
            }
        });
    }

}
