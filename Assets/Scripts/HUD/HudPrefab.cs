using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制伤害效果的生成，附在Canvas上
/// </summary>
public class HudPrefab : MonoBehaviour
{
    /// <summary>
    /// 文字
    /// </summary>
    private Text hudText;

    /// <summary>
    /// 生成伤害文字
    /// </summary>
    public void HUD(float damageDef)
    {
        hudText = GetComponentInChildren<Text>();
        hudText.text = "-" + damageDef.ToString();
        Invoke("DestroySelf",1f);
    }

    private void DestroySelf()
    {
        GameObjectPoolManager.Instance.Recycle(transform.parent.gameObject);
    }
}