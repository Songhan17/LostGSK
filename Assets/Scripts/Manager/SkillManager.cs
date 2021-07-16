using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillManager : Singleton<SkillManager>
{

    /// <summary>
    ///  技能信息的列表（集合）
    /// </summary>
    private Dictionary<int, Skill> skillDict;


    /// <summary>
    /// 解析技能信息
    /// </summary>
    public void ParseSkillJson()
    {
        skillDict = new Dictionary<int, Skill>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset skillText = Resources.Load<TextAsset>("Data/Skill/Skills");
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
            int consume = (int)(temp["consume"].n);
            int damage = (int)(temp["damage"].n);
            int defense = (int)(temp["defense"].n);
            string description = temp["description"].str;

            Skill skill = null;
            switch (type)
            {
                case Skill.SkillType.Red:
                    // 红特有
                    float duration = (temp["duration"].n);
                    string animId = temp["animId"].str;
                    skill = new RedSkill(id, name, type, amount, consume, damage, defense, description,false, duration, animId);
                    break;
                case Skill.SkillType.Blue:
                    // 蓝特有
                    animId = temp["animId"].str;
                    float restore = temp["restore"].n;
                    skill = new BlueSkill(id, name, type, amount, consume, damage, defense, description,false, animId, restore);
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
                    skill = new YellowSkill(id, name, type, amount, consume, damage, defense, description,false, animId, restore, addHp,
                       addMp, addAtk, addDef, reduceCon, addSpe, addLck);
                    break;
                case Skill.SkillType.White:
                    // 白特有
                    skill = new WhiteSkill(id, name, type, amount, consume, damage, defense, description, false);
                    break;
            }
            skillDict.Add(skill.Id, skill);

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

    // 获取所有信息
    public List<Skill> GetSkills()
    {
        return skillDict.Values.ToList();
    }

    public Dictionary<int, Skill> GetDict()
    {
        return skillDict;
    }

    public Skill GetSkillByName(string name)
    {
        foreach (Skill item in skillDict.Values.ToList())
        {
            if (item.Name.Equals(name))
            {
                return item;
            }
        }
        return null;
    }

}
