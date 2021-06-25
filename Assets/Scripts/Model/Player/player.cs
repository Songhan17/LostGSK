using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    

    // 人物面板,初始、存档记录
    public int Id { get; set; }
    // 生命值HP
    public float Hp { get; set; }
    // 蓝量MP
    public float Mp { get; set; }
    // 攻击力ATK
    public float Atk { get; set; }
    // 防御力DEF
    public float Def { get; set; }
    // 移动速度SPE
    public float Spe { get; set; }
    // 幸运值LCK
    public float Lck { get; set; }
    // 恢复速率-mp
    public float Restore { get; set; }

    public Player(int id,float hp, float mp, float atk, float def, float spe, float lck, float restore)
    {
        Id = id;
        Hp = hp;
        Mp = mp;
        Atk = atk;
        Def = def;
        Spe = spe;
        Lck = lck;
        Restore = restore;
    }

    public override string ToString()
    {
        return "生命："+Hp+";"+ "能量：" + Mp + ";" + "攻击：" + Atk + ";" + "防御：" + Def
            + ";" + "移动速度：" + Spe + ";" + "幸运值：" + Lck + ";" + "恢复速率/s：" + Restore;
    }
}
