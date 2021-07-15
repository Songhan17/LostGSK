using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{

    public Player_SO playerSO;
    public PlayerTemplate_SO template_SO;

    //[Header("生命值HP")]
    [HideInInspector]
    public int Hp { get => playerSO.hp; set => playerSO.hp = value; }
    //[Header("当前生命值HP")]
    [HideInInspector]
    public int CurrentHp {
        get => Mathf.Max(playerSO.currentHp, 0);
        set => playerSO.currentHp = Mathf.Clamp(value, 0, 1000);
    }
    //[Header("蓝量MP")]
    [HideInInspector]
    public int Mp { get => playerSO.mp; set => playerSO.mp = value; }
    //[Header("当前蓝量MP")]
    [HideInInspector]
    public int CurrentMp { get => playerSO.currentMp; set => playerSO.currentMp = Mathf.Clamp(value, 0, 1000); }
    //[Header("攻击力ATK")]
    [HideInInspector]
    public int Atk { get => playerSO.atk; set => playerSO.atk = value; }
    //[Header("防御力DEF")]
    [HideInInspector]
    public int Def { get => playerSO.def; set => playerSO.def = value; }
    //[Header("移动速度SPE")]
    [HideInInspector]
    public int Spe { get => playerSO.spe; set => playerSO.spe = value; }
    //[Header("幸运值LCK")]
    [HideInInspector]
    public int Lck { get => playerSO.lck; set => playerSO.lck = value; }
    //[Header("恢复速率-mp")]
    [HideInInspector]
    public float Restore { get => playerSO.restore; set => playerSO.restore = value; }
    //[Header("符卡值")]
    [HideInInspector]
    public int Money { get => playerSO.money; set => playerSO.money = value; }

    public RedSkill RedSkill { get => (RedSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }
    public BlueSkill BlueSkill { get => (BlueSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Blue); }
    public YellowSkill YellowSkill { get => (YellowSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.Yellow); }
    public WhiteSkill WhiteSkill { get => (WhiteSkill)SkillController.Instance.GetSkillEquip(Skill.SkillType.White); }


    public override string ToString()
    {
        return "生命：" + CurrentHp + "/" + Hp + ";" + "能量：" + CurrentMp + "/" + Mp + ";" + "攻击：" + Atk + ";"
            + "防御：" + Def + ";" + "移动速度：" + Spe + ";" + "幸运值：" + Lck + ";" + "恢复速率/s：" + Restore;
    }

    private void OnEnable()
    {
        //playerSO = new Player_SO(template_SO.hp, template_SO.currentHp, template_SO.mp, template_SO.currentMp,
        //    template_SO.atk, template_SO.def, template_SO.spe, template_SO.lck, template_SO.restore, template_SO.money);
        playerSO = ScriptableObject.CreateInstance<Player_SO>();
        playerSO.hp = template_SO.hp;
        playerSO.currentHp = template_SO.currentHp;
        playerSO.mp = template_SO.mp;
        playerSO.currentMp = template_SO.currentMp;
        playerSO.atk = template_SO.atk;
        playerSO.def = template_SO.def;
        playerSO.spe = template_SO.spe;
        playerSO.lck = template_SO.lck;
        playerSO.restore = template_SO.restore;
        playerSO.money = template_SO.money;
    }

}
