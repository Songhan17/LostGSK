using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkill : Skill
{
    /// <summary>
    /// 持续时间-f
    /// </summary>
    public float Duration { get; set; }
    /// <summary>
    /// 动画id-i,序号，置于融合树中？
    /// </summary>
    public string AnimId { get; set; }

    public RedSkill(int id, string name, SkillType skillType, int amount, int consume,
        int damage, int defense, string des, bool isEquip, float duration, string animId)
        : base(id, name, skillType, amount, consume, damage, defense, des,isEquip)
    {
        Duration = duration;
        AnimId = animId;
    }
}
