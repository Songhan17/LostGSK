
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    private volatile List<Skill> skillList;

    // 获取技能列表
    public List<Skill> GetList(Skill.SkillType type)
    {
        List<Skill> skills = new List<Skill>();
        skillList.ForEach(e =>
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
        skillList.Add(SkillManager.Instance.GetSkillById(id));
    }



}
