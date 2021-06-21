using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSkill : Skill
{
    public string AnimId { get; set; }
    public float Restore { get; set; }
    public float AddHp { get; set; }
    public float AddMp { get; set; }
    public float AddAtk { get; set; }
    public float AddDef { get; set; }
    public float ReduceCon { get; set; }
    public float AddSpe { get; set; }
    public float AddLck { get; set; }

    public YellowSkill(int id, string name, SkillType skillType, int amount, float consume,
        float damage, float defense, string des, bool isEquip, string animId, float restore, float addHp, float addMp, float addAtk,
        float addDef, float reduceCon, float addSpe, float addLck)
        : base(id, name, skillType, amount, consume, damage, defense, des,isEquip)
    {
        AnimId = animId;
        Restore = restore;
        AddHp = addHp;
        AddMp = addMp;
        AddAtk = addAtk;
        AddDef = addDef;
        ReduceCon = reduceCon;
        AddSpe = addSpe;
        AddLck = addLck;
    }
}
