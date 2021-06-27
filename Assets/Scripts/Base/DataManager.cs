using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{

    public Player_SO playerSO;

    //[Header("生命值HP")]
    [HideInInspector]
    public float Hp { get => playerSO.hp; set => playerSO.hp = value; }
    //[Header("当前生命值HP")]
    [HideInInspector]
    public float CurrentHp { get => playerSO.currentHp; set => playerSO.currentHp = value; }
    //[Header("蓝量MP")]
    [HideInInspector]
    public float Mp { get => playerSO.mp; set => playerSO.mp = value; }
    //[Header("当前蓝量MP")]
    [HideInInspector]
    public float CurrentMp { get => playerSO.currentMp; set => playerSO.currentMp = value; }
    //[Header("攻击力ATK")]
    [HideInInspector]
    public float Atk { get => playerSO.atk; set => playerSO.atk = value; }
    //[Header("防御力DEF")]
    [HideInInspector]
    public float Def { get => playerSO.def; set => playerSO.def = value; }
    //[Header("移动速度SPE")]
    [HideInInspector]
    public float Spe { get => playerSO.spe; set => playerSO.spe = value; }
    //[Header("幸运值LCK")]
    [HideInInspector]
    public float Lck { get => playerSO.lck; set => playerSO.lck = value; }
    //[Header("恢复速率-mp")]
    [HideInInspector]
    public float Restore { get => playerSO.restore; set => playerSO.restore = value; }
    //[Header("符卡值")]
    [HideInInspector]
    public float Money { get => playerSO.money; set => playerSO.money = value; }

    public RedSkill RedSkill { get => (RedSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }
    public BlueSkill BlueSkill { get => (BlueSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Blue); }
    public YellowSkill YellowSkill { get => (YellowSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Yellow); }
    public WhiteSkill WhiteSkill { get => (WhiteSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.White); }


    public override string ToString()
    {
        return "生命：" + CurrentHp+ "/"+ Hp + ";" + "能量：" +CurrentMp +"/"+ Mp + ";" + "攻击：" + Atk + ";"
            + "防御：" + Def + ";" + "移动速度：" + Spe + ";" + "幸运值：" + Lck + ";" + "恢复速率/s：" + Restore;
    }

}
