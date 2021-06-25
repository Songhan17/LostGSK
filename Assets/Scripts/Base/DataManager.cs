using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{

    public Player_SO playerSO;

    //[Header("生命值HP")]
    public float Hp { get; set; }
    //[Header("当前生命值HP")]
    public float CurrentHp;
    //[Header("蓝量MP")]
    public float Mp;
    //[Header("当前蓝量MP")]
    public float CurrentMp;
    //[Header("攻击力ATK")]
    public float Atk;
    //[Header("防御力DEF")]
    public float Def;
    //[Header("移动速度SPE")]
    public float Spe;
    //[Header("幸运值LCK")]
    public float Lck;
    //[Header("恢复速率-mp")]
    public float Restore;
    //[Header("符卡值")]
    public float Money;

    public Skill RedSkill { get => SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }
    public Skill BlueSkill { get => SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }
    public Skill YellowSkill { get => SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }
    public Skill WhiteSkill { get => SkillController.Instance.GetSkillEquip(Skill.SkillType.Red); }

    private void Awake()
    {
        Debug.Log(playerSO.hp);
    }


}
