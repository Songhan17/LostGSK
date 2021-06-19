using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    #region 单例模式
    private static SkillManager _instance;

    public static SkillManager Instance {
        get {
            if (_instance == null)
            {
                //下面的代码只会执行一次
                _instance = GameObject.Find("GameManager").GetComponent<SkillManager>();
            }
            return _instance;
        }
    }
    #endregion

    /// <summary>
    ///  技能信息的列表（集合）
    /// </summary>
    private Dictionary<int, Skill> skillDict;

    void Awake()
    {
        ParseSkillJson();
        
    }

    /// <summary>
    /// 解析技能信息
    /// </summary>
    public void ParseSkillJson()
    {
        skillDict = new Dictionary<int, Skill>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset skillText = Resources.Load<TextAsset>("SkillJson/skills");
        string skillsJson = skillText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(skillsJson);
        foreach (JSONObject temp in j.list)
        {
            string typeStr = temp["type"].str;
            Skill.SkillType type = (Skill.SkillType)System.Enum.Parse(typeof(Skill.SkillType), typeStr);

            //下面的事解析这个对象里面的公有属性
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            int amount = (int)(temp["amount"].n);
            float consume = (temp["consume"].n);
            float damage = (temp["damage"].n);
            float defense = (temp["defense"].n);
            string description = temp["description"].str;

            Skill skill = null;
            switch (type)
            {
                case Skill.SkillType.Red:
                    // 红特有
                    float duration = (temp["duration"].n);
                    string animId = temp["animId"].str;
                    skill = new RedSkill(id, name, type, amount, consume, damage, defense, description, duration, animId);
                    break;
                case Skill.SkillType.Blue:
                    // 蓝特有
                    animId = temp["animId"].str;
                    float restore = temp["restore"].n;
                    skill = new BlueSkill(id, name, type, amount, consume, damage, defense, description, animId, restore);
                    break;
                case Skill.SkillType.Yellow:
                    // 黄特有
                    animId = temp["animId"].str;
                    restore = temp["restore"].n;
                    float addHp = temp["addHp"].n;
                    float addMp = temp["addMp"].n;
                    float addAtk = temp["addAtk"].n;
                    float addDef = temp["addDef"].n;
                    float reduceCon = temp["reduceCon"].n;
                    float addSpe = temp["addSpe"].n;
                    float addLck = temp["addLck"].n;
                    skill = new YellowSkill(id, name, type, amount, consume, damage, defense, description, animId, restore, addHp,
                       addMp, addAtk, addDef, reduceCon, addSpe, addLck);
                    break;
                case Skill.SkillType.White:
                    // 白特有
                    skill = new WhiteSkill();
                    break;
            }
            skillDict.Add(skill.ID, skill);

        }
    }

    // 获取技能信息by id
    public Skill GetSkillById(int id)
    {
        if (skillDict != null)
        {
            return skillDict[id];
        }
        return null;
    }

    public List<Skill> GetSkills()
    {
        return skillDict.Values.ToList();
    }

}
