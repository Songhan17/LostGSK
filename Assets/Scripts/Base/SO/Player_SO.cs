using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player_SO : ScriptableObject
{
    [Header("生命值HP")]
    public float hp;
    [Header("当前生命值HP")]
    public float currentHp;
    [Header("蓝量MP")]
    public float mp;
    [Header("当前蓝量MP")]
    public float currentMp;
    [Header("攻击力ATK")]
    public float atk;
    [Header("防御力DEF")]
    public float def;
    [Header("移动速度SPE")]
    public float spe;
    [Header("幸运值LCK")]
    public float lck;
    [Header("恢复速率-mp")]
    public float restore;
    [Header("符卡值")]
    public float money;
 
}
