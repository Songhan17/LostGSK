using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region 单例模式
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //下面的代码只会执行一次
                _instance = GameObject.Find("GameManager").GetComponent<PlayerManager>();
            }
            return _instance;
        }
    }
    #endregion

    // 加载数据
    public void initData()
    {

    }

}
