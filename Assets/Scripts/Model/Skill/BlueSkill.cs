using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSkill : Skill
{
    public string AnimId { get; set; }
    /// <summary>
    /// 恢复值-/秒（部分特殊）
    /// </summary>
    public float Restore { get; set; }

    public BlueSkill(int id, string name, SkillType skillType, int amount, float consume,
        float damage, float def, string des, string animId, float restore)
        : base(id, name, skillType, amount, consume, damage, def, des)
    {
        AnimId = animId;
        Restore = restore;
    }
}
