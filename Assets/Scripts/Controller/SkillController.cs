
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoSingleton<SkillController>
{

    private Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();

    private void Awake()
    {
        Load();
    }

    // 获取技能信息by id
    public Skill GetSkillById(int id)
    {
        if (skillDict != null)
        {
            return skillDict[id];
        }
        return null;
    }

    public Skill GetSkillEquip(Skill.SkillType type)
    {
        Skill skill = null;
        skillDict.Values.ToList().ForEach(e =>
        {
            if (e.Type == type)
            {
                if (e.IsEquip)
                {
                    skill = e;
                    return;
                }
            }
        });
        return skill;
    }

    // 获取技能列表
    public List<Skill> GetList(Skill.SkillType type)
    {
        if (skillDict == null)
        {
            return null;
        }
        List<Skill> skills = new List<Skill>();
        skillDict.Values.ToList().ForEach(e =>
        {
            if (e.Type == type)
            {
                skills.Add(e);
            }
        });
        return skills;
    }

    public List<Skill> GetSkills()
    {
        return skillDict?.Values.ToList();
    }

    // 获得技能
    public void AddSkill(int id,bool isEquip)
    {
        Skill skill;
        if (skillDict.ContainsKey(id))
        {
            skill = skillDict[id];
            skill.Amount++;
            skillDict[id] = skill;
        }
        else
        {
            skill = SkillManager.Instance.GetSkillById(id);
            if (isEquip)
            {
                skill.IsEquip = true;
            }
            skillDict.Add(skill.Id, skill);
        }
    }

    // 初始化加载技能
    public void InitSkill()
    {
        // 读取配置文件
        SkillManager.Instance.GetSkills();
        SkillManager.Instance.GetDict();
    }

    public void EquipSkill(int id)
    {
        Skill skill = SkillManager.Instance.GetSkillById(id);
        // 释放技能按键
        if (Input.GetKeyDown(KeyCode.E))
        {
            //1 播放对应技能动画
            // animatorController
            //2 角色面板修改--减少mp
        }
        // 常规面板属性增减
    }

    public void UpdateSkillDict(List<Skill> skills)
    {
        skills?.ForEach(s =>
        {
            skillDict[s.Id] = s;
        });
    }

    public void Save()
    {
        Debug.Log("Save");
        if (skillDict.Count == 0)
        {
            return;
        }
        string skillData = "";

        skillDict.Values.ToList().ForEach(sk =>
        {
            skillData +=  ((sk.IsEquip ? -1 : 1) * sk.Id).ToString() + "A";
        });
        PlayerPrefs.SetString("Player_Skill", skillData.TrimEnd('A'));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Player_Skill") == false) return;
        string skillsData = PlayerPrefs.GetString("Player_Skill");
        Debug.Log("Load");
        //Debug.Log(skillsData);
        if (skillsData == null)
        {
            return;
        }
        List<string> list = new List<string>(skillsData.Split('A'));

        list.ForEach(e =>
        {
            //Debug.Log(e);
            if (int.Parse(e) < 0)
            {
                AddSkill(-int.Parse(e), true);
            }
            else
            {
                AddSkill(int.Parse(e), false);
            }
        });

    }

}
