using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSkill : Skill
{
    public WhiteSkill(int id, string name, SkillType type, int amount, int consume,
        int damage, int def, string des, bool isEquip)
        : base(id, name, type, amount, consume, damage, def, des, isEquip)
    {

    }
}
