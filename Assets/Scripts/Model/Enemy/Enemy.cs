using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Hp { get; set; }
    /// <summary>
    /// 触碰伤害-f
    /// </summary>
    public int TouchDamage { get; set; }
    /// <summary>
    /// 标准伤害量-f
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// 防御量-f
    /// </summary>
    public int Defense { get; set; }

    public string Description { get; set; }
    //掉落对应skill ID序列
    public int Drop { get; set; }
    public string DropType { get; set; }
    public string AnimId { get; set; }

    public Enemy(int iD, string name, string type, int hp, int touchDamage, int damage, int def, string des, int drop, string dType, string animId)
    {
        ID = iD;
        Name = name;
        Type = type;
        Hp = hp;
        TouchDamage = touchDamage;
        Damage = damage;
        Defense = def;
        Description = des;
        Drop = drop;
        DropType = dType;
        AnimId = animId;
    }

    public Enemy()
    {
    }
}
