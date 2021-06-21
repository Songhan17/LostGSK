using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameSpellCardList : JuiSingletonExtension<UIGameSpellCardList>
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

    public int LastStartIndex {
        get => Math.Max(skills.Count - maxItemCount, 0);
    }
    private int startIndex;
    public int StartIndex {
        get => startIndex;
        set {
            value = Math.Max(Math.Min(value, LastStartIndex), 0);
            startIndex = value;
            //if (this.LastStartIndex != 0 && !isDragingScroll) //防止除数为0，滚动条没在手动调整
            //{
            //    this.scrollbar.value = (float)value / (float)this.LastStartIndex;
            //}

            for (int i = 0; i < maxItemCount; i++)
            {
                if (value + i >= skills.Count)
                {
                    childItems[i].text.text = string.Empty;
                    childItems[i].item.gameObject.SetActive(false);
                    continue;
                }
                Skill skill = skills[value + i];
                if (skill.Name.Equals(UIGameSpellCard.Instance.CurrentSkill(skill)))
                {
                    childItems[i].equip.isOn = true;
                }
                else
                {
                    childItems[i].equip.isOn = false;
                }
                childItems[i].text.text = skill.Name;
                childItems[i].item.gameObject.SetActive(true);
            }
        }
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        group = transform.Find("List");
        maxItemCount = group.childCount;
        childItems = new ChildItem[maxItemCount];
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
                if (!skill.IsEquip)
                {
                    itemRoot.Find("Toggle").GetComponent<Toggle>().isOn = true;
                    UIGameSpellCard.Instance.EquipCard(skill);
                    UpdateSkill(skill,true);
                }
                else
                {
                    itemRoot.Find("Toggle").GetComponent<Toggle>().isOn = false;
                    UIGameSpellCard.Instance.Unstall(skill);
                    UpdateSkill(skill, false);
                }
            });

        }

        group.GetChild(maxItemCount - 1).AddListener(EventTriggerType.Move, e =>
         {
             if (Input.GetKeyDown(KeyCode.S))
             {
                 StartIndex++;
                 Debug.Log(startIndex);
                 EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(maxItemCount - 1).gameObject);
             }
         });
        group.GetChild(0).AddListener(EventTriggerType.Move, e =>
         {
             if (Input.GetKeyDown(KeyCode.W))
             {
                 StartIndex--;
                 Debug.Log(startIndex);
                 EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(0).gameObject);
             }
         });




    }

   

    protected override void OnShow()
    {
        if (skills == null || skills.Capacity == 0)
        {
            Hide();
            return;
        }
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
                if (skill.Name.Equals(UIGameSpellCard.Instance.CurrentSkill(skill)))
                {
                    childItems[i].equip.isOn = true;
                }
                else
                {
                    childItems[i].equip.isOn = false;
                }
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
        StartIndex = 0;
    }

    public override void Hide()
    {
        base.Hide();
        for (int i = 0; i < maxItemCount; i++)
        {
            if (i >= skills.Count)
            {
                childItems[i].text.text = string.Empty;
                childItems[i].item.gameObject.SetActive(false);
                continue;
            }
            childItems[i].equip.isOn = false;
        }
        EventSystemManager.Instance.SetCurrentGameObject(lastTransform.gameObject);
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
        // TODO
        Navigation navigation = EventSystemManager.Instance.GetCurrent().GetComponent<Button>().navigation;
        Debug.Log(navigation.selectOnDown.gameObject.activeInHierarchy);

    }

    public void Refresh(Skill.SkillType type, Transform last)
    {
        lastTransform = last;
        skills?.Clear();
        SkillController skillController = GameObject.Find("Player").GetComponent<SkillController>();
        skills = skillController.GetList(type);
    }

    public void UpdateSkill(Skill skill, bool isEquip)
    {
        skills.ForEach(i =>
        {
            if (i.Id == skill.Id)
            {
                i.IsEquip = isEquip;
            }
            else
            {
                i.IsEquip = false;
            }
        });
    }

}
