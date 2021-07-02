using UnityEngine;
using System.Collections;

/// <summary>
/// 技能基类
/// </summary>
public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SkillType Type { get; set; }
    public int Amount { get; set; }
    /// <summary>
    /// 消耗量-f
    /// </summary>
    public float Consume { get; set; }
    /// <summary>
    /// 伤害量-f
    /// </summary>
    public float Damage { get; set; }
    /// <summary>
    /// 防御量-f
    /// </summary>
    public float Defense { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// 是否装备，标志位
    /// </summary>
    public bool IsEquip { get; set; }

    public Skill() { Id = -1; }

    public Skill(int id, string name, SkillType type, int amount, float consume,
        float damage, float def, string des, bool isEquip)
    {
        Id = id;
        Name = name;
        Type = type;
        Amount = amount;
        Consume = consume;
        Damage = damage;
        Defense = def;
        Description = des;
        IsEquip = isEquip;
    }
}
/// <summary>
/// 技能类型
/// </summary>
public enum SkillType
{
    Red,
    Blue,
    Yellow,
    White
}