
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    private Dictionary<int, Skill> skillDict;

    // 获取技能列表
    public List<Skill> GetList(Skill.SkillType type)
    {

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

    // 获得技能
    public void GetSkill(int id)
    {
        Skill skill = skillDict[id];
        if (skill != null)
        {
            skill.Amount++;
            skillDict.Remove(id);
            skillDict.Add(skill.Id, skill);
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

}
