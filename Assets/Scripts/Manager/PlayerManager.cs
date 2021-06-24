using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{

    private Dictionary<int, Player> playerDict;

    void Awake()
    {
        ParsePlayerJson();
    }

    /// <summary>
    /// 解析人物信息
    /// </summary>
    public void ParsePlayerJson()
    {
        playerDict = new Dictionary<int, Player>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset playerText = Resources.Load<TextAsset>("Data/Player/Player");
        string playerJson = playerText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(playerJson);
        foreach (JSONObject temp in j.list)
        {
            //下面的事解析这个对象里面的公有属性
            int id = (int)(temp["id"].n);
            float hp = (temp["hp"].n);
            float mp = (temp["mp"].n);
            float atk = (temp["atk"].n);
            float def = (temp["def"].n);
            float spe = (temp["spe"].n);
            float lck = (temp["lck"].n);
            float restore = (temp["restore"].n);
            Player player = new Player(id, hp, mp, atk, def, spe, lck, restore);
            Debug.Log(player.ToString());
            playerDict.Add(player.Id, player);
        }
    }

    // 获取人物信息by id
    public Player GetPlayerById(int id)
    {
        if (playerDict != null)
        {
            return playerDict[id];
        }
        return null;
    }

}
