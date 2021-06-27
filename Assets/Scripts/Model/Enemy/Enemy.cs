using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public float Hp { get; set; }
    /// <summary>
    /// 触碰伤害-f
    /// </summary>
    public float TouchDamage { get; set; }
    /// <summary>
    /// 标准伤害量-f
    /// </summary>
    public float Damage { get; set; }
    /// <summary>
    /// 防御量-f
    /// </summary>
    public float Defense { get; set; }

    public string Description { get; set; }
    //掉落对应skill ID序列
    public int Drop { get; set; }
    public string DropType { get; set; }
    public string AnimId { get; set; }

    public Enemy(int iD, string name, string type, float hp, float touchDamage, float damage, float def, string des, int drop, string dType, string animId)
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
}
