using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    /// <summary>
    ///  敌人信息的列表（集合）
    /// </summary>
    private List<Enemy> enemyList;

    void Awake()
    {
        ParseEnemyJson();

    }

    // 解析敌人类信息
    public void ParseEnemyJson()
    {
        enemyList = new List<Enemy>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset enemyText = Resources.Load<TextAsset>("Data/Enemy/Enemy");
        string enemyJson = enemyText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(enemyJson);
        foreach (JSONObject temp in j.list)
        {
            //下面的事解析这个对象里面的公有属性
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            string type = temp["type"].str;
            float hp = temp["hp"].n;
            float touchDamage = temp["touchDamage"].n;
            float damage = temp["damage"].n;
            float defense = temp["defense"].n;
            string description = temp["description"].str;
            int drop = (int)(temp["drop"].n);
            string dType = temp["dType"].str;
            string animId = temp["animId"].str;

            Enemy enemy = null;

            enemy = new Enemy(id, name, type, hp, touchDamage, damage, defense, description, drop, dType, animId);

            enemyList.Add(enemy);

        }

    }

    public Enemy GetEnemyById(int id)
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.ID == id)
            {
                return enemy;
            }
        }
        return null;
    }

}
