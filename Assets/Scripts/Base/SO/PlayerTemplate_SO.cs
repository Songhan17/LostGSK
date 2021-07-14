using UnityEngine;

[CreateAssetMenu]
public class PlayerTemplate_SO : ScriptableObject
{
    [Header("生命值HP")]
    [Range(0, 1000)]
    public int hp;
    [Header("当前生命值HP")]
    [Range(0, 1000)]
    public int currentHp;
    [Header("蓝量MP")]
    [Range(0, 1000)]
    public int mp;
    [Header("当前蓝量MP")]
    [Range(0, 1000)]
    public int currentMp;
    [Header("攻击力ATK")]
    public int atk;
    [Header("防御力DEF")]
    public int def;
    [Header("移动速度SPE")]
    public int spe;
    [Header("幸运值LCK")]
    public int lck;
    [Header("恢复速率-mp")]
    public float restore;
    [Header("符卡值")]
    public int money;

}
