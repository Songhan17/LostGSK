using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSpellCardList : JuiSingleton<UIGameSpellCardList>
{
    public override string uiPath => "MenuPanel/SpellCard/CardList";

    private int maxItemCount;
    private Transform group;
    private List<Skill> skills;
    private ChildItem[] childItems;
    private Transform lastTransform;
    private class ChildItem
    {
        public Transform item;
        public Toggle equip;
        public Text text;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        group = transform.Find("List");
        maxItemCount = group.childCount;
        childItems = new ChildItem[maxItemCount];
        Debug.Log(maxItemCount);
        for (int i = 0; i < maxItemCount; i++)
        {
            Transform itemRoot = group.GetChild(i);
            childItems[i] = new ChildItem()
            {
                item = itemRoot,
                equip = itemRoot.Find("Toggle").GetComponent<Toggle>(),
                text = itemRoot.Find("Text").GetComponent<Text>()
            };
            itemRoot.GetComponent<Button>().onClick.AddListener(() =>
            {
                var skill = SkillManager.Instance.GetSkillByName(itemRoot.Find("Text").GetComponent<Text>().text);
                itemRoot.Find("Toggle").GetComponent<Toggle>().isOn = true;
                UIGameSpellCard.Instance.EquipCard(skill);
            });
        }
    }

    protected override void OnShow()
    {
        base.OnShow();
        for (int i = 0; i < maxItemCount; i++)
        {
            if (skills != null && skills.Capacity > 0)
            {
                if (i >= skills.Count)
                {
                    childItems[i].text.text = string.Empty;
                    childItems[i].item.gameObject.SetActive(false);
                    continue;
                }
                Skill skill = skills[i];
                childItems[i].text.text = skill.Name;
                childItems[i].item.gameObject.SetActive(true);
            }
            else
            {
                childItems[i].text.text = string.Empty;
                childItems[i].item.gameObject.SetActive(false);
            }
        }
        EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(0).gameObject);
    }

    public override void Hide()
    {
        base.Hide();
        EventSystemManager.Instance.SetCurrentGameObject(lastTransform.gameObject);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

    }

    public void Refresh(Skill.SkillType type, Transform last)
    {
        lastTransform = last;
        skills?.Clear();
        SkillController skillController = GameObject.Find("Player").GetComponent<SkillController>();
        skills = skillController.GetList(type);
    }



}
