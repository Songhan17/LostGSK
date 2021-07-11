using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制伤害显示
/// </summary>
public class HUDText : MonoBehaviour
{
    /// <summary>
    /// 滚动速度
    /// </summary>
    private readonly float speed = 1f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0f;

  
    private void Update()
    {
        Scroll();
    }

    /// <summary>
    /// 冒泡效果
    /// </summary>
    private void Scroll()
    {
        //字体滚动
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        //字体渐变透明
        GetComponent<Text>().color = new Color(1, 0, 0, 1 - timer);

    }

    private void OnDisable()
    {
        timer = 0;
        GetComponent<Text>().color = new Color(1, 0, 0, 1);
        transform.position = transform.parent.position;
    }

}