
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
    public void GetSkill(int id)
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

        // TODO 写入json
        Save();
        //skillDict.Clear();
        //skillDict = skills.ToDictionary(item => item.Id, item => item);
    }

    private void Save()
    {
        Debug.Log(skillDict.Count);
        string skllsJson = JsonConvert.SerializeObject(skillDict);
        PlayerPrefs.SetString("Player_Skill", skllsJson);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Player_Skill") == false) return;
        string skillsData = PlayerPrefs.GetString("Player_Skill");
        Debug.Log(skillsData);
        skillDict = (Dictionary<int, Skill>)JsonConvert.DeserializeObject(skillsData);
        skillDict.Values.ToList().ForEach(e =>
        {
            Debug.Log(e.Name);
        });
    }

}
