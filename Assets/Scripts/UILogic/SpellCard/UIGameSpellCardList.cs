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
    private Transform endChild;
    // 上一次已装备的位置
    private int? lastIndex;
    // 已装备的id
    private int equipId;
    private Slider slider;
    private class ChildItem
    {
        public Transform item;
        public Toggle equip;
        public Text text;
    }

    // 最大索引
    public int MaxStartIndex {
        get => Math.Max(skills.Count - maxItemCount, 0);
    }
    // 滑动索引
    private int startIndex;
    public int StartIndex {
        get => startIndex;
        set {
            value = Math.Max(Math.Min(value, MaxStartIndex), 0);
            startIndex = value;

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

    // 当前导航索引
    public int? CurrentIndex {
        get => GetIndexByName(EventSystemManager.Instance.GetCurrent().GetComponentInChildren<Text>().text);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        group = transform.Find("List");
        slider = transform.Find("Slider").GetComponent<Slider>();
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
                    UIGameSpellCard.Instance.Refresh(skill,true);
                    UpdateSkill(skill, true);
                    equipId = skill.Id;
                }
                else
                {
                    itemRoot.Find("Toggle").GetComponent<Toggle>().isOn = false;
                    UIGameSpellCard.Instance.Refresh(skill,false);
                    UpdateSkill(skill, false);
                    equipId = 0;
                }
            });

        }
        #region 0位监听
        group.GetChild(0).AddListener(EventTriggerType.Move, e =>
         {
             if (Input.GetKeyDown(KeyCode.W))
             {
                 Debug.Log(CurrentIndex);
                 if (CurrentIndex == 0)
                 {
                     EventSystemManager.Instance.SetCurrentGameObject(endChild.gameObject);
                     StartIndex = MaxStartIndex;
                 }
                 else
                 {
                     StartIndex--;
                 }
             }
         });
        #endregion
    }



    protected override void OnShow()
    {
        
        base.OnShow();
        if (skills == null || skills.Count == 0)
        {
            base.Hide();
            return;
        }
        slider.maxValue = skills.Count-1;
        if (skills.Count <= maxItemCount)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
        #region 记录上一次路径
        if (lastIndex != -1)
        {
            StartIndex = Math.Max(lastIndex.Value - (maxItemCount - 1), 0);
            if (StartIndex > 0)
            {
                EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(maxItemCount - 1).gameObject);
            }
            else
            {
                EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(lastIndex.Value).gameObject);
            }
        }
        else
        {
            StartIndex = 0;
            EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(0).gameObject);
        }
        #endregion
        endChild = group.GetChild(group.GetChildActive() - 1);

        #region 返回最上级监听
        if (!endChild.name.Equals(group.GetChild(0).name))
        {
            endChild.AddListener(EventTriggerType.Move, e =>
         {
             if (Input.GetKeyDown(KeyCode.S))
             {
                 if (MaxStartIndex <= 0)
                 {
                     if (EventSystemManager.Instance.GetCurrent() == endChild.gameObject)
                     {
                         EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(0).gameObject);
                     }
                 }
                 else
                 {
                     if (startIndex == MaxStartIndex)
                     {
                         StartIndex = 0;
                         EventSystemManager.Instance.SetCurrentGameObject(group.GetChild(0).gameObject);
                     }
                     else
                     {
                         StartIndex++;
                     }
                 }
             }
         });
        }
        #endregion
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
        // 移除非0位监听
        if (!endChild.name.Equals(group.GetChild(0).name))
        {
            endChild.RemoveAllListener(EventTriggerType.Move);
        }
        UIGameSpellCard.Instance.UpdateSkill(equipId);
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
        slider.value = (float)CurrentIndex;
    }

    // 刷新UI
    public void Refresh(Skill.SkillType type, Transform last, string text)
    {
        lastTransform = last;
        skills?.Clear();
        skills = UIGameSpellCard.Instance.GetList(type);
        lastIndex = GetIndexByName(text);
    }

    // 更新技能，保存数据
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

    // 见名知意
    public int? GetIndexByName(string name)
    {
        skills?.ForEach(s => {
            if (s.Name == name) equipId = s.Id;
        });
        return skills?.FindIndex(item =>
             item.Name.Equals(name));
    }

}
